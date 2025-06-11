using System.Security.Claims;
using AppAbstract.Users;
using AppCore.BusinessEntities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Accounts;

namespace Portal.Controllers;

/// <summary>
/// Controller responsible for registering and logging users
/// </summary>
[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountsManager _accounts;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public AccountController(IAccountsManager accounts, IMapper mapper)
    {
        _accounts = accounts ?? throw new ArgumentNullException(nameof(accounts));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Create an account
    /// </summary>
    /// <param name="registerViewModel"></param>
    /// <returns>User token valid 1 day</returns>
    [HttpPost("registerJvt")]
    public async Task<ActionResult<AuthenticationResponseViewModel>> RegisterJvt(
        [FromBody] RegisterViewModel registerViewModel)
    {
        var result = await _accounts.RegisterAndGetJwtKey(_mapper.Map<UserCredentials>(registerViewModel));

        if (result is IJwtTokenWithMessage tokenWithMessage && tokenWithMessage.Message.Any())
            return BadRequest(tokenWithMessage.Message);

        return _mapper.Map<AuthenticationResponseViewModel>(result);
    }

    /// <summary>
    /// Login to an existing account
    /// </summary>
    /// <param name="registerViewModel"></param>
    /// <returns>User token valid 1 day</returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseViewModel>> LoginJvt(
        [FromBody] LoginViewModel registerViewModel)
    {
        var result = await _accounts.LoginAndGetJwtKey(_mapper.Map<UserCredentials>(registerViewModel));

        if (result is IJwtTokenWithMessage tokenWithMessage && tokenWithMessage.Message.Any())
            return BadRequest(tokenWithMessage.Message);

        return _mapper.Map<AuthenticationResponseViewModel>(result);
    }

    /// <summary>
    /// Create an account ar
    /// </summary>
    /// <param name="registerViewModel"></param>
    /// <returns>cookie with user claims</returns>
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponseViewModel>> Register(
        [FromBody] RegisterViewModel registerViewModel)
    {
        var claims = await _accounts.Register(_mapper.Map<UserCredentials>(registerViewModel));
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true, // Refreshing the authentication session should be allowed.
            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

            //IsPersistent = true,
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return LocalRedirect("/");  
    }
}
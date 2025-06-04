using AppAbstract.Users;
using AppCore.BusinessEntities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Accounts;

namespace ShopPortal.Controllers;

/// <summary>
/// sample comment
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
        _accounts = accounts ?? throw  new ArgumentNullException(nameof(accounts));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Create an account
    /// </summary>
    /// <param name="userCredentialsViewModel"></param>
    /// <returns>User token valid 1 day</returns>
    [HttpPost("create")]
    public async Task<ActionResult<AuthenticationResponseViewModel>> Create([FromBody] UserCredentialsViewModel userCredentialsViewModel)
    {
        var result = await _accounts.RegisterAndGetJwtKey(_mapper.Map<UserCredentials>(userCredentialsViewModel));

        if(result is IJwtTokenWithMessage tokenWithMessage && tokenWithMessage.Message.Any())
            return BadRequest(tokenWithMessage.Message);
            
        return _mapper.Map<AuthenticationResponseViewModel>(result);
    }

    /// <summary>
    /// Login to an existing account
    /// </summary>
    /// <param name="userCredentialsViewModel"></param>
    /// <returns>User token valid 1 day</returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseViewModel>> Login([FromBody] UserCredentialsViewModel userCredentialsViewModel)
    {
        var result = await _accounts.LoginAndGetJwtKey(_mapper.Map<UserCredentials>(userCredentialsViewModel));

        if(result is IJwtTokenWithMessage tokenWithMessage && tokenWithMessage.Message.Any())
            return BadRequest(tokenWithMessage.Message);
            
        return _mapper.Map<AuthenticationResponseViewModel>(result);
    }
    
    /// <summary>
    /// Create an account
    /// </summary>
    /// <param name="userCredentialsViewModel"></param>
    /// <returns>User token valid 1 day</returns>
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponseViewModel>> Register([FromBody] UserCredentialsViewModel userCredentialsViewModel)
    {
        var result = await _accounts.Register(_mapper.Map<UserCredentials>(userCredentialsViewModel));
       //HttpContext.SignInAsync( CookieAuthenticationDefaults.AuthenticationScheme, )
        
        
        if(result is IJwtTokenWithMessage tokenWithMessage && tokenWithMessage.Message.Any())
            return BadRequest(tokenWithMessage.Message);
            
        return _mapper.Map<AuthenticationResponseViewModel>(result);
    }
}
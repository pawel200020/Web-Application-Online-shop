using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppAbstract.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AppCore.Users;

public class AccountsManager(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IConfiguration config) : IAccountsManager
{
    private readonly UserManager<IdentityUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly SignInManager<IdentityUser> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly IConfiguration _config = config ?? throw new ArgumentNullException(nameof(config));

    public async Task<IJwtTokenWithExpirationDate> RegisterAndGetJwtKey(IUserCredentials userCredentials)
    {
        var user = new IdentityUser() { UserName = userCredentials.Email, Email = userCredentials.Email};
        var result =  await _userManager.CreateAsync(user, userCredentials.Password);

        if (result.Succeeded)
            return new JwtTokenWithExpirationDate(await BuildToken(userCredentials)) ;

        return new JwtTokenWithMessage(result.Errors.Select(x => x.Description));
    }

    public async Task<IJwtTokenWithExpirationDate> LoginAndGetJwtKey(IUserCredentials userCredentials)
    {
        var result = await _signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded)
            return new JwtTokenWithExpirationDate(await BuildToken(userCredentials));

        return new JwtTokenWithMessage(["Incorrect user login or password, please try again."]);
    }
    
    private async Task<IJwtTokenWithExpirationDate> BuildToken(IUserCredentials userCredentialsViewModel)
    {
        var claims = new List<Claim>()
        {
            new ("email", userCredentialsViewModel.Email)
        };
        var user = await _userManager.FindByNameAsync(userCredentialsViewModel.Email);
        var claimsDb = await _userManager.GetClaimsAsync(user);

        claims.AddRange(claimsDb);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["keyjwt"] ?? throw new InvalidOperationException()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddDays(1);
        var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration,
            signingCredentials: creds);
        return new JwtTokenWithExpirationDate(new JwtSecurityTokenHandler().WriteToken(token), expiration);
    }

    public async Task <IEnumerable<Claim>> Register(IUserCredentials userCredentials)
    {
        var user = new IdentityUser()
        {
            UserName = userCredentials.Login, 
            Email = userCredentials.Email,
            PhoneNumber = userCredentials.PhoneNumber
            
        };
        var result =  await _userManager.CreateAsync(user, userCredentials.Password);

        if(result.Succeeded)
            return new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, UserRole.Administrator.ToString()),
        };
        return [];
    }

    public Task Login(IUserCredentials userCredentials)
    {
        throw new NotImplementedException();
    }
}
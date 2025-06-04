using System.Security.Claims;
namespace AppAbstract.Users;

public interface IAccountsManager
{
    Task<IJwtTokenWithExpirationDate> RegisterAndGetJwtKey(IUserCredentials userCredentials);
    Task<IJwtTokenWithExpirationDate> LoginAndGetJwtKey(IUserCredentials userCredentials);
    Task<IEnumerable<Claim>> Register(IUserCredentials userCredentials);
    Task Login(IUserCredentials userCredentials);
}
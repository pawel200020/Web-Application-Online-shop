using AppAbstract.Users;

namespace AppCore.Users;

internal class JwtTokenWithExpirationDate : IJwtTokenWithExpirationDate
{
    public JwtTokenWithExpirationDate(IJwtTokenWithExpirationDate jwtTokenWithExpirationDate)
    {
        Token = jwtTokenWithExpirationDate.Token;
        ExpirationDate = jwtTokenWithExpirationDate.ExpirationDate;
    }
    public JwtTokenWithExpirationDate(string token, DateTime expirationDate)
    {
        Token = token;
        ExpirationDate = expirationDate;
    }
    public string? Token { get; }
    public DateTime ExpirationDate { get;}
}
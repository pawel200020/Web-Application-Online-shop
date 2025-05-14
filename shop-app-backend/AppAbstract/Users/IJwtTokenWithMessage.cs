namespace AppAbstract.Users;

public interface IJwtTokenWithMessage : IJwtTokenWithExpirationDate
{
    IEnumerable<string> Message { get; }
}
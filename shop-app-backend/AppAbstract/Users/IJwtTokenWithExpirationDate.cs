namespace AppAbstract.Users;

public interface IJwtTokenWithExpirationDate
{
    public string? Token { get; }
    public DateTime ExpirationDate { get; }
}
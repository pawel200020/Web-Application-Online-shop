namespace AppAbstract.Users;

public interface IUserCredentials
{
    public string Email { get; }
    public string Password { get; }
}
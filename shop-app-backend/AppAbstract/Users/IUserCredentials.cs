namespace AppAbstract.Users;

public interface IUserCredentials
{
    public string Email { get; } 
    
    public string Login { get; } 
    
    public DateTime DateOfBirth { get; }
    
    public string? PhoneNumber { get; }

    public string Password { get; }
}
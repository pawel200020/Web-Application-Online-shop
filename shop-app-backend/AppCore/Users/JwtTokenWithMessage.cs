using AppAbstract.Users;

namespace AppCore.Users;

internal class JwtTokenWithMessage : IJwtTokenWithMessage
{
    public JwtTokenWithMessage()
    { }
    public JwtTokenWithMessage(IEnumerable<string> messages)
    {
        Message = messages;
    }
    public string? Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public IEnumerable<string> Message { get; set; }
}
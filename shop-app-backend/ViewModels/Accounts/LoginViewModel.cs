using System.ComponentModel.DataAnnotations;

namespace ViewModels.Accounts;

public class LoginViewModel
{
    [Required] 
    public string Login { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
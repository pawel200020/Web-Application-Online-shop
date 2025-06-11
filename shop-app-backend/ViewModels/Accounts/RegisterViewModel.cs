using System.ComponentModel.DataAnnotations;
#nullable enable
namespace ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = null!;

        [Required] 
        public string Login { get; set; } = null!;

        [Required] 
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using AppAbstract.Users;

namespace AppCore.BusinessEntities
{
    public class UserCredentials : IUserCredentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Login { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

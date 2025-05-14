using System.ComponentModel.DataAnnotations;
using AppAbstract.Users;

namespace AppCore.BusinessEntities
{
    public class UserCredentials : IUserCredentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

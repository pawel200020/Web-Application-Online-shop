using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities.Users;

public class Avatar
{
    public Avatar(string userId)
    {
        UserId = userId;
    }

    public int Id { get; set; }
    public byte? Image { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; } = null!;
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime TsInsert { get; private set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime TsUpdate { get; private set; }
    
}
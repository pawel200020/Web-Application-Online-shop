using AppAbstract.Store;
using Microsoft.AspNetCore.Identity;

namespace AppCore.BusinessEntities;

public class Rating : IRating
{
    public int Id { get; set; }
    public int Rate { get; set; }
    public int ProductId { get; set; }
    public IProduct Product { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
}
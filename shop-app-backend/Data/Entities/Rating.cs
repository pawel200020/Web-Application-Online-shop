using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppAbstract.Store;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class Rating : IRating
    {
        public int Id { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [NotMapped]
        IProduct IRating.Product => Product;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppAbstract.Store;
using AppAbstract.Store.Denpendecies;

namespace Data.Entities.Dependencies
{
    public class OrdersProducts : IOrdersProducts
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Quantity { get; set; }
        public Order Order { get; set; } = null!;
        public Product Product { get; set; }
        [NotMapped] IOrder IOrdersProducts.Order => Order;
        [NotMapped] IProduct IOrdersProducts.Product => Product;

    }
}

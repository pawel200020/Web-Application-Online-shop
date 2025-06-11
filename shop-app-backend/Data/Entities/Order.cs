using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppAbstract.Store;
using AppAbstract.Store.Denpendencies;
using Data.Entities.Dependencies;
using Data.Validation;


namespace Data.Entities
{
    public class Order : IOrder
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        
        public IEnumerable<OrdersProducts> OrdersProducts { get; set; } = null!;

        [NotMapped]
        IEnumerable<IOrdersProducts> IOrder.OrdersProducts
        {
            get => OrdersProducts;
            set => OrdersProducts = (value as IEnumerable<OrdersProducts>)!;
        }
    }
}

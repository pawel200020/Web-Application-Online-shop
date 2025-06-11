using AppAbstract.Store.Denpendencies;

namespace Data.Entities.Dependencies
{
    public class ProductsOrders : IProductsOrders
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Picture { get; set; }
        public int Quantity { get; set; }
    }
}

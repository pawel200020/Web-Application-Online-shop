using AppAbstract.Store;

namespace Data.Entities.Dependencies
{
    public class ProductsCategories : IProductsCategories
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}

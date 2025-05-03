using AppAbstract.Store;

namespace AppCore.BusinessEntities
{
    public class ProductPutGet : IProductPutGet
    {
        public IProduct Product { get; set; } = null!;
        public IEnumerable<ICategory> SelectedCategories { get; set; } = null!;
        public IEnumerable<ICategory>  NonSelectedCategories { get; set; } = null!;
    }
}

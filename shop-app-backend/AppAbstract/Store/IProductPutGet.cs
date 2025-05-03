namespace AppAbstract.Store;

public interface IProductPutGet
{
    public IProduct Product { get; } 
    public IEnumerable<ICategory>  SelectedCategories { get; } 
    public IEnumerable<ICategory>  NonSelectedCategories { get; } 
}
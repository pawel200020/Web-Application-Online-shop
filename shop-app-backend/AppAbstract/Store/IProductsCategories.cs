namespace AppAbstract.Store;
#nullable enable
public class IProductsCategories
{
    public int CategoryId { get; }
    public int ProductId { get; }
    public ICategory Category { get; } 
    public IProduct Product { get; } 
}
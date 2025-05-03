namespace AppAbstract.Store.Denpendencies;

public interface IProductsOrders
{
    public int Id { get; }
    public string Name { get; }
    public string? Picture { get; }
    public int Quantity { get; }
}
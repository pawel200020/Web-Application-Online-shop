namespace AppAbstract.Store.Denpendencies;

public interface IOrdersProducts
{
    public int OrderId { get; }
    public int ProductId { get; }
    public int Quantity { get; }
    public IOrder Order { get; }
    public IProduct Product { get; }
}
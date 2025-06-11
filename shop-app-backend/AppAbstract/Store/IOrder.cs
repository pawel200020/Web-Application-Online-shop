using AppAbstract.Store.Denpendencies;

namespace AppAbstract.Store;

public interface IOrder
{
    int Id { get; }
    string Name { get; }
    double Value { get; }
    IEnumerable<IOrdersProducts> OrdersProducts { get; set; }
}
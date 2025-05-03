using AppAbstract.Store;
using AppCore.BusinessEntities;
using Data.Entities;

namespace AppCore.Store;

public interface IOrdersManager
{
    Task<(IOrder[] orders, int quanitity)> GetAll(PaginationModel paginationModel);
    Task<IOrder> GetById(int id);
    Task<int> AddOrder(Order order);
    Task Delete(int id);
}
using AppAbstract.Pagination;
using AppAbstract.Store;
using Data.Entities;

namespace Data.Abstract;

public interface IOrdersRepository
{
    Task<IOrder?> GetById(int id);
    public IQueryable<IOrder> GetOrdersAsQueryable();
    Task<int> AddOrder(IOrder order);
    Task Delete(int id);
}
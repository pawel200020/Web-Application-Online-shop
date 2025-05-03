using AppAbstract.Store;
using AppAbstract.Store.Denpendencies;

namespace Data.Abstract;

public interface IProductsRepository
{
    Task<IProductsOrders[]> SearchByName(string name);
    Task<IProduct?> GetById(int id);
    IQueryable<IProduct> GetProductsAsQueryable();
    Task<IProduct?> GetByIdWithCurrentUserRank(int id, string? email);
    Task Save(IProduct product);
    Task Save(IProduct product, int existingItemId);
    Task Delete(int id);
}

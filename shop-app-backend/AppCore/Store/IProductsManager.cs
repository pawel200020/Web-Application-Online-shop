using AppAbstract.Store;
using AppAbstract.Store.Denpendencies;
using AppCore.BusinessEntities;

namespace AppCore.Store;

public interface IProductsManager
{
    Task<IProductsOrders[]> SearchByName(string name);
    Task<IEnumerable<ICategory>> GetEmptyProductWithAllCategories();
    Task<(IProduct[]products, int quantity)> FilterWithCriteria(FilterProducts filterProducts);
    Task<(IProduct[] products, int quantity)> Get(PaginationModel paginationModel);
    Task<IProduct?> GetById(int id, string? email);
    Task Create(IProduct product);
    Task<IProductPutGet> PrepareForEdit(int id, string? email);
    Task Save(int id, IProduct editedProduct);
    Task Delete(int id);
}
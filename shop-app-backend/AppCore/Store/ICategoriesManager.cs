using AppAbstract.Store;
using AppCore.BusinessEntities;

namespace AppCore.Store;

public interface ICategoriesManager
{
    Task<(ICategory[] categories, int quanitity)> GetAllCategoriesPaged(PaginationModel paginationModel);
    Task<IEnumerable<ICategory>> GetAllCategories();
    Task<ICategory?> GetById(int id);
    Task Create(ICategory category);
    Task Edit(int id, ICategory newCategory);
    Task Delete(int id);
}
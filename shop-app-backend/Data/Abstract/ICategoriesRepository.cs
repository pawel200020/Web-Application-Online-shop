using AppAbstract.Pagination;
using AppAbstract.Store;

namespace Data.Abstract;

public interface ICategoriesRepository
{
    Task<ICategory[]> GetAllCategoriesPaged(IPaginationModel paginationModel);
    Task<IEnumerable<ICategory>> GetAllCategories();
    Task<IEnumerable<ICategory>> GetALlCategories();
    Task<ICategory?> GetById(int id);
    Task Create(ICategory category);
    Task Edit(int id, ICategory newCategory);
    Task Delete(int id);
    Task<IEnumerable<ICategory>> GetSelectedCategories(IEnumerable<int> categoriesIds);
    Task<IEnumerable<ICategory>> GetNonSelectedCategories(IEnumerable<int> categoriesIds);
}
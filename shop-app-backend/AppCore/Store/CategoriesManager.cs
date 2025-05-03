using AppAbstract.Store;
using AppCommonTools.Linq;
using AppCore.BusinessEntities;
using Data.Abstract;

namespace AppCore.Store;

internal class CategoriesManager(ICategoriesRepository categoriesRepository) : ICategoriesManager
{
    private readonly ICategoriesRepository _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));

    public async Task<(ICategory[] categories, int quanitity)> GetAllCategoriesPaged(PaginationModel paginationModel)
        {
            var categories = (await _categoriesRepository.GetAllCategories())
                .OrderBy(x => x.Name).AsQueryable().Paginate(paginationModel).ToArray();
            return  (categories,  categories.Length);
        }

        public async Task<IEnumerable<ICategory>> GetAllCategories()
            => (await _categoriesRepository.GetAllCategories()).OrderBy(x=>x.Name);

        public async Task<ICategory?> GetById(int id)
            => await _categoriesRepository.GetById(id);

        public async Task Create(ICategory category)
            => await _categoriesRepository.Create(category);

        public async Task Edit(int id, ICategory newCategory)
            => await _categoriesRepository.Edit(id, newCategory);

        public async Task Delete(int id)
            => await _categoriesRepository.Delete(id);
}
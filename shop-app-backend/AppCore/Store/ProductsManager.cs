using AppAbstract.Store;
using AppAbstract.Store.Denpendencies;
using AppCore.BusinessEntities;
using AppCore.Extensions;
using Data.Abstract;
using Microsoft.EntityFrameworkCore;
using Product = Data.Entities.Product;

namespace AppCore.Store;

public class ProductsManager : IProductsManager
{
    private readonly IProductsRepository _productsRepository;
    private readonly ICategoriesRepository _categoriesRepository;

    public ProductsManager(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository)
    {
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
    }

    public async Task<IProductsOrders[]> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return [];
        
        return await _productsRepository.SearchByName(name);
    }

    public async Task<IEnumerable<ICategory>> GetEmptyProductWithAllCategories()
        => await _categoriesRepository.GetALlCategories();

    public async Task<(IProduct[]products, int quantity)> FilterWithCriteria(FilterProducts filterProducts)
    {
        var productsQueryable = _productsRepository.GetProductsAsQueryable();
        if (!string.IsNullOrWhiteSpace(filterProducts.Name))
            productsQueryable = productsQueryable.Where(x => x.Name.Contains(filterProducts.Name));

        if (filterProducts.isAvalible)
        {
            productsQueryable = productsQueryable.Where(x => x.IsAvalible);
        }

        if (filterProducts.CategoryId != -1)
        {
            productsQueryable = productsQueryable
                .Where(x => x.ProductsCategories.Select(y => y.CategoryId)
                    .Contains(filterProducts.CategoryId));
        }

        var products = await productsQueryable.OrderBy(x => x.Name).Paginate(filterProducts.PaginationModel)
            .ToArrayAsync();
        return (products, await productsQueryable.CountAsync());
    }

    public async Task<(IProduct[] products, int quantity)> Get(PaginationModel paginationModel)
    {
        var queryable = _productsRepository.GetProductsAsQueryable();
        IProduct[] products = await queryable.OrderBy(x => x.Name).Paginate(paginationModel).ToArrayAsync();
        return (products, await queryable.CountAsync());
    }

    public async Task<IProduct?> GetById(int id, string? email)
        => await _productsRepository.GetByIdWithCurrentUserRank(id, email);

    public async Task Create(IProduct product) 
        => await _productsRepository.Save(product);

    public async Task<IProductPutGet> PrepareForEdit(int id, string? email)
    {
        var product = await GetById(id, email);

        var categoriesSelectedIds =
            product?.ProductsCategories?.Select(x => x.ProductId).ToArray() ?? Array.Empty<int>();
        var selectedCategories = await _categoriesRepository.GetSelectedCategories(categoriesSelectedIds);
        var nonSelectedCategories = await _categoriesRepository.GetNonSelectedCategories(categoriesSelectedIds);

        return new ProductPutGet()
        {
            Product = product ?? throw new ArgumentNullException(nameof(product), "Product not found in database"),
            SelectedCategories = selectedCategories,
            NonSelectedCategories = nonSelectedCategories
        };
    }

    public async Task Save(int id, IProduct editedProduct) 
        => await _productsRepository.Save(editedProduct, id);

    public async Task Delete(int id) 
        => await _productsRepository.Delete(id);
}
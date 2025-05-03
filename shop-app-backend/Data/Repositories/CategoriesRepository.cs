using AppAbstract.Pagination;
using AppAbstract.Store;
using AppCommonTools.Linq;
using Data.Abstract;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories;

internal class CategoriesRepository(ApplicationDbContext context, ILogger<CategoriesRepository> logger) : ICategoriesRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<CategoriesRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<ICategory[]> GetAllCategoriesPaged(IPaginationModel paginationModel)
    {
        _logger.LogInformation("Getting all categories paginated");
        var queryable = _context.Categories.AsQueryable();
        return await queryable.Paginate(paginationModel).ToArrayAsync();
    }

    public async Task<IEnumerable<ICategory>> GetAllCategories()
    {
        _logger.LogInformation("Getting all categories");
        return _context.Categories.AsQueryable();
    }

    public async Task<ICategory?> GetById(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Create(ICategory category)
    {
        _context.Categories.Add((category as Category)!);
        await _context.SaveChangesAsync();
    }

    public async Task Edit(int id, ICategory newCategory)
    {
        ICategory? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            throw new InvalidOperationException("trying to edit not existing category");
        category = newCategory;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var exitst = await _context.Categories.AnyAsync(x => x.Id == id);
        if (!exitst)
            throw new InvalidOperationException("trying to remove not existing category");

        _context.Remove(new Category() { Id = id });
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ICategory>> GetSelectedCategories(IEnumerable<int> categoriesIds)
        => await _context.Categories.Where(x => categoriesIds.Contains(x.Id)).ToArrayAsync();

    public async Task<IEnumerable<ICategory>> GetNonSelectedCategories(IEnumerable<int> categoriesIds)
        => await _context.Categories.Where(x => !categoriesIds.Contains(x.Id)).ToArrayAsync();

    public async Task<IEnumerable<ICategory>> GetALlCategories()
        => await _context.Categories.ToArrayAsync();
}
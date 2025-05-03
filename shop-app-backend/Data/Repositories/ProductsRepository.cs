using AppAbstract.Services;
using AppAbstract.Store;
using AppAbstract.Store.Denpendencies;
using Data.Abstract;
using Data.Entities;
using Data.Entities.Dependencies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
internal class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IFileStorageService _fileStorageService;
    private const string ContainerName = "products";

    public ProductsRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager,
        IFileStorageService fileStorageService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
    }
    
    public async Task<IProductsOrders[]> SearchByName(string name) =>
        await _context.Products.Where(x => x.Name
                .Contains(name))
            .Where(x => x.IsAvalible)
            .OrderBy(x => x.Name)
            .Select(x => new ProductsOrders {Id = x.Id, Name = x.Name, Picture = x.Picture})
            .Take(5)
            .ToArrayAsync();

    public IQueryable<IProduct> GetProductsAsQueryable() => _context.Products.AsQueryable();
    
    public async Task<IProduct?> GetById(int id)
    =>   await _context.Products
        .Include(x => x.ProductsCategories)
        .ThenInclude(x => x.Category)
        .FirstOrDefaultAsync(x => x.Id == id);
    public async Task<IProduct?> GetByIdWithCurrentUserRank(int id, string? email)
    {
        var product = await GetById(id) as Product;

        if (product == null)
            return null;

        var avgVote = 0.0;
        var userVote = 0;

        if (await _context.Rating.AnyAsync(x => x.ProductId == id))
        {
            avgVote = await _context.Rating.Where(x => x.ProductId == id).AverageAsync(x => x.Rate);
            if (email is not null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var userId = user.Id;

                var ratingDb =
                    await _context.Rating.FirstOrDefaultAsync(x => x.ProductId == id && x.UserId == userId);
                if (ratingDb != null)
                {
                    userVote = ratingDb.Rate;
                }
            }
        }

        product.AverageVote = avgVote;
        product.UserVote = userVote;
        return product;
    }
    
    public async Task Save(IProduct product)
    {
        if (product.PictureFile != null)
             await _fileStorageService.SaveFile(ContainerName, product.PictureFile);

        _context.Add(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task Save(IProduct product, int existingItemId)
    {
        var productFromDb = await _context.Products
            .Include(x => x.ProductsCategories)
            .ThenInclude(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == existingItemId);

        if (productFromDb is null)
            throw new InvalidOperationException("trying to edit not existing product");

        if (product.PictureFile != null)
            productFromDb.Picture = await _fileStorageService.SaveFile(ContainerName, product.PictureFile);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
            throw new InvalidOperationException("product which you attempted to remove does not exits");

        _context.Remove(product);
        await _context.SaveChangesAsync();
    }
}
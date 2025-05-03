using AppAbstract.Store;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RatingsRepository : IRatingsRepository
{
    private readonly ApplicationDbContext _context;
    public RatingsRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IRating?> GetCurrentUserRate(int productId, string userId) => await _context.Rating.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

    public async Task AddRating(IRating rating)
    {
        await _context.AddAsync(rating);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRating(IRating rating)
    {
        _context.Rating.Update(rating as Rating);
        await _context.SaveChangesAsync();
    }
}
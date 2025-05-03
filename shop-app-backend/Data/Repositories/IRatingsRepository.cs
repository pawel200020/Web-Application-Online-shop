using AppAbstract.Store;

namespace Data.Repositories;

public interface IRatingsRepository
{
    Task<IRating?> GetCurrentUserRate(int productId, string userId);
    Task AddRating(IRating rating);
    Task UpdateRating(IRating rating);
}
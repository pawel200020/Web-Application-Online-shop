using Data;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AppCore.Store;

public class RatingsManager : IRatingsManager
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IRatingsRepository _ratingsRepository;
    public RatingsManager(ApplicationDbContext context, UserManager<IdentityUser> userManager, IRatingsRepository ratingsRepository)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _ratingsRepository = ratingsRepository ?? throw new ArgumentNullException(nameof(ratingsRepository));
    }

    public async Task Vote(Rating rating, string? email)
    {
        if (email == null) 
            throw new ArgumentNullException(nameof(email));
        var userId = (await _userManager.FindByNameAsync(email)).Id;
        var currentRate = await _ratingsRepository.GetCurrentUserRate(rating.ProductId, userId) as Rating; 
        if (currentRate == null)
        {
            var newRating = new Rating()
            {
                ProductId = rating.ProductId,
                Rate = rating.Rate,
                UserId = userId,
            };
            await _ratingsRepository.AddRating(newRating);
        }
        else
        {
            currentRate.Rate = rating.Rate;
            await _ratingsRepository.UpdateRating(currentRate);
        }
    }
}
using Data.Entities;

namespace AppCore.Store;

public interface IRatingsManager
{
    Task Vote(Rating rating, string? email);
}
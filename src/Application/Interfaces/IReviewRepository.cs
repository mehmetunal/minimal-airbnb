using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Review Repository Interface
/// </summary>
public interface IReviewRepository
{
    /// <summary>
    /// Eve göre yorumları getir
    /// </summary>
    Task<IEnumerable<Review>> GetByPropertyIdAsync(Guid propertyId);
    
    /// <summary>
    /// Misafire göre yorumları getir
    /// </summary>
    Task<IEnumerable<Review>> GetByGuestIdAsync(Guid guestId);
    
    /// <summary>
    /// Puana göre yorumları getir
    /// </summary>
    Task<IEnumerable<Review>> GetByRatingAsync(int rating);
} 
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Review Repository Interface
/// </summary>
public interface IReviewRepository
{
    /// <summary>
    /// Değerlendirme ekler
    /// </summary>
    Task<Review> AddAsync(Review review);

    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <returns>Kaydedilen değişiklik sayısı</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Ev ID'sine göre değerlendirmeleri getir
    /// </summary>
    Task<IEnumerable<Review>> GetByPropertyIdAsync(Guid propertyId);

    /// <summary>
    /// Kullanıcı ID'sine göre değerlendirmeleri getir
    /// </summary>
    Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId);
} 
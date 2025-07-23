using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Review Repository Interface
/// </summary>
public interface IReviewRepository
{
    /// <summary>
    /// Tüm review'ları getir (IQueryable)
    /// </summary>
    IQueryable<Review> GetAll();

    /// <summary>
    /// Tüm review'ları getir
    /// </summary>
    Task<IEnumerable<Review>> GetAllAsync();

    /// <summary>
    /// ID'ye göre review getir
    /// </summary>
    Task<Review?> GetByIdAsync(Guid id);

    /// <summary>
    /// Property'ye göre review'ları getir
    /// </summary>
    Task<IEnumerable<Review>> GetByPropertyAsync(Guid propertyId);

    /// <summary>
    /// Kullanıcıya göre review'ları getir
    /// </summary>
    Task<IEnumerable<Review>> GetByUserAsync(Guid userId);

    /// <summary>
    /// Property ve kullanıcıya göre review getir
    /// </summary>
    Task<Review?> GetByPropertyAndUserAsync(Guid propertyId, Guid userId);

    /// <summary>
    /// Review ekle
    /// </summary>
    Task<Review> AddAsync(Review review);

    /// <summary>
    /// Review güncelle
    /// </summary>
    Task<Review> UpdateAsync(Review review);

    /// <summary>
    /// Review sil
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Favorite Repository Interface
/// </summary>
public interface IFavoriteRepository
{
    /// <summary>
    /// Favori ekler
    /// </summary>
    Task<Favorite> AddAsync(Favorite favorite);

    /// <summary>
    /// Favori siler
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <returns>Kaydedilen değişiklik sayısı</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Kullanıcı ID'sine göre favorileri getir
    /// </summary>
    Task<IEnumerable<Favorite>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Ev ID'sine göre favorileri getir
    /// </summary>
    Task<IEnumerable<Favorite>> GetByPropertyIdAsync(Guid propertyId);

    /// <summary>
    /// Kullanıcının favori evlerini getir
    /// </summary>
    Task<IEnumerable<Property>> GetFavoritePropertiesAsync(Guid userId);

    /// <summary>
    /// Favori var mı kontrol et
    /// </summary>
    Task<bool> ExistsAsync(Guid userId, Guid propertyId);
} 
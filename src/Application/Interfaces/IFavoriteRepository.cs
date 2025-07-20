using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Favorite Repository Interface
/// </summary>
public interface IFavoriteRepository
{
    /// <summary>
    /// Kullanıcının favorilerini getir
    /// </summary>
    Task<IEnumerable<Favorite>> GetByUserIdAsync(Guid userId);
    
    /// <summary>
    /// Evin favori sayısını getir
    /// </summary>
    Task<int> GetFavoriteCountByPropertyIdAsync(Guid propertyId);
    
    /// <summary>
    /// Kullanıcının evi favori olarak ekleyip eklemediğini kontrol et
    /// </summary>
    Task<bool> IsFavoriteAsync(Guid userId, Guid propertyId);
    
    /// <summary>
    /// Favori ekle veya kaldır (toggle)
    /// </summary>
    Task<bool> ToggleFavoriteAsync(Guid userId, Guid propertyId);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
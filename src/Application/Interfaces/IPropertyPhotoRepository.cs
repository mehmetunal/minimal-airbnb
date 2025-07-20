using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// PropertyPhoto Repository Interface
/// </summary>
public interface IPropertyPhotoRepository
{
    /// <summary>
    /// Eve göre fotoğrafları getir
    /// </summary>
    Task<IEnumerable<PropertyPhoto>> GetByPropertyIdAsync(Guid propertyId);
    
    /// <summary>
    /// Ana fotoğrafı getir
    /// </summary>
    Task<PropertyPhoto?> GetMainPhotoByPropertyIdAsync(Guid propertyId);
    
    /// <summary>
    /// Ana fotoğrafı ayarla
    /// </summary>
    Task SetMainPhotoAsync(Guid propertyId, Guid photoId);
    
    /// <summary>
    /// Fotoğraf sıralamasını güncelle
    /// </summary>
    Task UpdatePhotoOrderAsync(Guid propertyId, Dictionary<Guid, int> photoOrders);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// PropertyPhoto Repository Interface
/// </summary>
public interface IPropertyPhotoRepository
{
    /// <summary>
    /// Fotoğraf ekler
    /// </summary>
    Task<PropertyPhoto> AddAsync(PropertyPhoto photo);

    /// <summary>
    /// Fotoğraf siler
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <returns>Kaydedilen değişiklik sayısı</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Ev ID'sine göre fotoğrafları getir
    /// </summary>
    Task<IEnumerable<PropertyPhoto>> GetByPropertyIdAsync(Guid propertyId);

    /// <summary>
    /// Ana fotoğrafı getir
    /// </summary>
    Task<PropertyPhoto?> GetMainPhotoAsync(Guid propertyId);
} 
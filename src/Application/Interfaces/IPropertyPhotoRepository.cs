using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// PropertyPhoto Repository Interface
/// </summary>
public interface IPropertyPhotoRepository
{
    /// <summary>
    /// Tüm fotoğrafları getirir
    /// </summary>
    IQueryable<Domain.Entities.PropertyPhoto> GetAll();

    /// <summary>
    /// ID'ye göre fotoğraf getirir
    /// </summary>
    Task<Domain.Entities.PropertyPhoto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Fotoğraf ekler
    /// </summary>
    Task<Domain.Entities.PropertyPhoto> AddAsync(Domain.Entities.PropertyPhoto photo);

    /// <summary>
    /// Fotoğraf günceller
    /// </summary>
    Task<Domain.Entities.PropertyPhoto> UpdateAsync(Domain.Entities.PropertyPhoto photo);

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
    Task<IEnumerable<Domain.Entities.PropertyPhoto>> GetByPropertyIdAsync(Guid propertyId);

    /// <summary>
    /// Ana fotoğrafı getir
    /// </summary>
    Task<Domain.Entities.PropertyPhoto?> GetMainPhotoAsync(Guid propertyId);
} 
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Property Repository Interface
/// </summary>
public interface IPropertyRepository
{
    /// <summary>
    /// Ev sahibine göre evleri getir
    /// </summary>
    Task<IEnumerable<Property>> GetByHostIdAsync(Guid hostId);
    
    /// <summary>
    /// Şehre göre evleri getir
    /// </summary>
    Task<IEnumerable<Property>> GetByCityAsync(string city);
    
    /// <summary>
    /// Ev tipine göre evleri getir
    /// </summary>
    Task<IEnumerable<Property>> GetByPropertyTypeAsync(PropertyType propertyType);
    
    /// <summary>
    /// Fiyat aralığına göre evleri getir
    /// </summary>
    Task<IEnumerable<Property>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    
    /// <summary>
    /// Misafir sayısına göre evleri getir
    /// </summary>
    Task<IEnumerable<Property>> GetByGuestCountAsync(int guestCount);
    
    /// <summary>
    /// Yayınlanmış evleri getir
    /// </summary>
    Task<IEnumerable<Property>> GetPublishedPropertiesAsync();
    
    /// <summary>
    /// Arama kriterlerine göre evleri getir
    /// </summary>
    Task<IEnumerable<Property>> SearchPropertiesAsync(string? city = null, PropertyType? propertyType = null, 
        decimal? minPrice = null, decimal? maxPrice = null, int? guestCount = null, bool? hasWifi = null, 
        bool? hasAirConditioning = null, bool? hasKitchen = null, bool? hasParking = null, bool? hasPool = null, 
        bool? allowsPets = null, bool? allowsSmoking = null);
} 
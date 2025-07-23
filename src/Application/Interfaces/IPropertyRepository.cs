using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Property Repository Interface
/// </summary>
public interface IPropertyRepository
{
    /// <summary>
    /// Tüm property'leri getir (IQueryable)
    /// </summary>
    IQueryable<Property> GetAll();

    /// <summary>
    /// Tüm property'leri getir
    /// </summary>
    /// <returns>Property listesi</returns>
    Task<IEnumerable<Property>> GetAllAsync();

    /// <summary>
    /// ID'ye göre property getir
    /// </summary>
    /// <param name="id">Property ID</param>
    /// <returns>Property</returns>
    Task<Property?> GetByIdAsync(Guid id);

    /// <summary>
    /// Property ekler
    /// </summary>
    /// <param name="property">Eklenecek property</param>
    /// <returns>Eklenen property</returns>
    Task<Property> AddAsync(Property property);

    /// <summary>
    /// Property günceller
    /// </summary>
    /// <param name="property">Güncellenecek property</param>
    /// <returns>Güncellenen property</returns>
    Task<Property> UpdateAsync(Property property);

    /// <summary>
    /// Property siler
    /// </summary>
    /// <param name="id">Silinecek property ID</param>
    /// <returns>Task</returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <returns>Kaydedilen değişiklik sayısı</returns>
    Task<int> SaveChangesAsync();

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

    /// <summary>
    /// Belirli tarih aralığında müsait evleri getir
    /// </summary>
    Task<IEnumerable<Property>> GetAvailablePropertiesAsync(DateTime checkInDate, DateTime checkOutDate, int guestCount);
} 
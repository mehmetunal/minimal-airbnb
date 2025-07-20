using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Property;

/// <summary>
/// Property Güncelleme DTO
/// </summary>
public class UpdatePropertyDto
{
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev açıklaması
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev adresi
    /// </summary>
    public string Address { get; set; } = string.Empty;
    
    /// <summary>
    /// Şehir
    /// </summary>
    public string City { get; set; } = string.Empty;
    
    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; } = string.Empty;
    
    /// <summary>
    /// Posta kodu
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Enlem
    /// </summary>
    public decimal Latitude { get; set; }
    
    /// <summary>
    /// Boylam
    /// </summary>
    public decimal Longitude { get; set; }
    
    /// <summary>
    /// Gecelik fiyat
    /// </summary>
    public decimal PricePerNight { get; set; }
    
    /// <summary>
    /// Temizlik ücreti
    /// </summary>
    public decimal CleaningFee { get; set; }
    
    /// <summary>
    /// Hizmet ücreti
    /// </summary>
    public decimal ServiceFee { get; set; }
    
    /// <summary>
    /// Maksimum misafir sayısı
    /// </summary>
    public int MaxGuests { get; set; }
    
    /// <summary>
    /// Yatak odası sayısı
    /// </summary>
    public int Bedrooms { get; set; }
    
    /// <summary>
    /// Yatak sayısı
    /// </summary>
    public int Beds { get; set; }
    
    /// <summary>
    /// Banyo sayısı
    /// </summary>
    public int Bathrooms { get; set; }
    
    /// <summary>
    /// Ev tipi
    /// </summary>
    public PropertyType PropertyType { get; set; }
    
    /// <summary>
    /// WiFi var mı?
    /// </summary>
    public bool HasWifi { get; set; }
    
    /// <summary>
    /// Klima var mı?
    /// </summary>
    public bool HasAirConditioning { get; set; }
    
    /// <summary>
    /// Mutfak var mı?
    /// </summary>
    public bool HasKitchen { get; set; }
    
    /// <summary>
    /// Otopark var mı?
    /// </summary>
    public bool HasParking { get; set; }
    
    /// <summary>
    /// Havuz var mı?
    /// </summary>
    public bool HasPool { get; set; }
    
    /// <summary>
    /// Evcil hayvan izni var mı?
    /// </summary>
    public bool AllowsPets { get; set; }
    
    /// <summary>
    /// Sigara izni var mı?
    /// </summary>
    public bool AllowsSmoking { get; set; }
    
    /// <summary>
    /// Minimum konaklama süresi
    /// </summary>
    public int MinimumStay { get; set; }
    
    /// <summary>
    /// Maksimum konaklama süresi
    /// </summary>
    public int MaximumStay { get; set; }
    
    /// <summary>
    /// Check-in saati
    /// </summary>
    public TimeSpan CheckInTime { get; set; }
    
    /// <summary>
    /// Check-out saati
    /// </summary>
    public TimeSpan CheckOutTime { get; set; }
    
    /// <summary>
    /// Ev kuralları
    /// </summary>
    public string HouseRules { get; set; } = string.Empty;
    
    /// <summary>
    /// Yayınlanmış mı?
    /// </summary>
    public bool IsPublished { get; set; }
} 
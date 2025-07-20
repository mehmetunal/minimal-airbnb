using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Property;

/// <summary>
/// Property Liste DTO
/// </summary>
public class PropertyListDto
{
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ev sahibi ID
    /// </summary>
    public Guid HostId { get; set; }
    
    /// <summary>
    /// Ev sahibi adı
    /// </summary>
    public string HostName { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Şehir
    /// </summary>
    public string City { get; set; } = string.Empty;
    
    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; } = string.Empty;
    
    /// <summary>
    /// Gecelik fiyat
    /// </summary>
    public decimal PricePerNight { get; set; }
    
    /// <summary>
    /// Maksimum misafir sayısı
    /// </summary>
    public int MaxGuests { get; set; }
    
    /// <summary>
    /// Yatak odası sayısı
    /// </summary>
    public int Bedrooms { get; set; }
    
    /// <summary>
    /// Banyo sayısı
    /// </summary>
    public int Bathrooms { get; set; }
    
    /// <summary>
    /// Ev tipi
    /// </summary>
    public PropertyType PropertyType { get; set; }
    
    /// <summary>
    /// Ortalama puan
    /// </summary>
    public decimal AverageRating { get; set; }
    
    /// <summary>
    /// Toplam yorum sayısı
    /// </summary>
    public int TotalReviews { get; set; }
    
    /// <summary>
    /// Ana fotoğraf URL'i
    /// </summary>
    public string? MainPhotoUrl { get; set; }
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
} 
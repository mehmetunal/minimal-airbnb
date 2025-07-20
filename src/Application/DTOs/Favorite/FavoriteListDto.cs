namespace MinimalAirbnb.Application.DTOs.Favorite;

/// <summary>
/// Favori Liste DTO
/// </summary>
public class FavoriteListDto
{
    /// <summary>
    /// Favori ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev sahibi adı
    /// </summary>
    public string HostName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gecelik fiyat
    /// </summary>
    public decimal PricePerNight { get; set; }
    
    /// <summary>
    /// Şehir
    /// </summary>
    public string City { get; set; } = string.Empty;
    
    /// <summary>
    /// Ana fotoğraf URL'i
    /// </summary>
    public string? MainPhotoUrl { get; set; }
    
    /// <summary>
    /// Ortalama puan
    /// </summary>
    public decimal AverageRating { get; set; }
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
} 
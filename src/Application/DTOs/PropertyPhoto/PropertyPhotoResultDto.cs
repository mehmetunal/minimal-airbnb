namespace MinimalAirbnb.Application.DTOs.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Sonuç DTO
/// </summary>
public class PropertyPhotoResultDto
{
    /// <summary>
    /// Fotoğraf ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;
    
    /// <summary>
    /// Fotoğraf URL'i
    /// </summary>
    public string PhotoUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Fotoğraf başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Fotoğraf açıklaması
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Ana fotoğraf mı?
    /// </summary>
    public bool IsMain { get; set; }
    
    /// <summary>
    /// Sıralama
    /// </summary>
    public int Order { get; set; }
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime UpdatedAt { get; set; }
} 
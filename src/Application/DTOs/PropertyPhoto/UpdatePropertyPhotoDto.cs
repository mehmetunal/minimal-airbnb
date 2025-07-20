namespace MinimalAirbnb.Application.DTOs.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Güncelleme DTO
/// </summary>
public class UpdatePropertyPhotoDto
{
    /// <summary>
    /// Fotoğraf ID
    /// </summary>
    public Guid Id { get; set; }
    
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
} 
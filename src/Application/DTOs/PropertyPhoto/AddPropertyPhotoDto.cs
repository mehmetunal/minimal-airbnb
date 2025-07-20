namespace MinimalAirbnb.Application.DTOs.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Ekleme DTO
/// </summary>
public class AddPropertyPhotoDto
{
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
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
    public bool IsMain { get; set; } = false;
    
    /// <summary>
    /// Sıralama
    /// </summary>
    public int Order { get; set; } = 0;
} 
namespace MinimalAirbnb.Application.DTOs.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Liste DTO
/// </summary>
public class PropertyPhotoListDto
{
    /// <summary>
    /// Fotoğraf ID
    /// </summary>
    public Guid Id { get; set; }
    
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
} 
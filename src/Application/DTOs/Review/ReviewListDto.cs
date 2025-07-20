namespace MinimalAirbnb.Application.DTOs.Review;

/// <summary>
/// Yorum Liste DTO
/// </summary>
public class ReviewListDto
{
    /// <summary>
    /// Yorum ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Misafir adı
    /// </summary>
    public string GuestName { get; set; } = string.Empty;
    
    /// <summary>
    /// Misafir profil fotoğrafı
    /// </summary>
    public string? GuestPhotoUrl { get; set; }
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;
    
    /// <summary>
    /// Puan (1-5)
    /// </summary>
    public int Rating { get; set; }
    
    /// <summary>
    /// Yorum başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Yorum içeriği (kısaltılmış)
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
} 
namespace MinimalAirbnb.Application.DTOs.Review;

/// <summary>
/// Yorum Güncelleme DTO
/// </summary>
public class UpdateReviewDto
{
    /// <summary>
    /// Yorum ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Puan (1-5)
    /// </summary>
    public int Rating { get; set; }
    
    /// <summary>
    /// Yorum başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Yorum içeriği
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Temizlik puanı
    /// </summary>
    public int CleanlinessRating { get; set; }
    
    /// <summary>
    /// İletişim puanı
    /// </summary>
    public int CommunicationRating { get; set; }
    
    /// <summary>
    /// Check-in puanı
    /// </summary>
    public int CheckInRating { get; set; }
    
    /// <summary>
    /// Doğruluk puanı
    /// </summary>
    public int AccuracyRating { get; set; }
    
    /// <summary>
    /// Konum puanı
    /// </summary>
    public int LocationRating { get; set; }
    
    /// <summary>
    /// Değer puanı
    /// </summary>
    public int ValueRating { get; set; }
} 
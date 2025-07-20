namespace MinimalAirbnb.Application.DTOs.Review;

/// <summary>
/// Yorum Sonuç DTO
/// </summary>
public class ReviewResultDto
{
    /// <summary>
    /// Yorum ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Misafir ID
    /// </summary>
    public Guid GuestId { get; set; }
    
    /// <summary>
    /// Misafir adı
    /// </summary>
    public string GuestName { get; set; } = string.Empty;
    
    /// <summary>
    /// Misafir profil fotoğrafı
    /// </summary>
    public string? GuestPhotoUrl { get; set; }
    
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;
    
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid ReservationId { get; set; }
    
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
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime UpdatedAt { get; set; }
} 
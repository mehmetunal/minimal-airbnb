using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Review;

/// <summary>
/// Değerlendirme Ekleme DTO
/// </summary>
public class AddReviewDto
{
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// Misafir ID
    /// </summary>
    public Guid GuestId { get; set; }
    
    /// <summary>
    /// Rezervasyon ID (opsiyonel)
    /// </summary>
    public Guid? ReservationId { get; set; }
    
    /// <summary>
    /// Puan (1-5)
    /// </summary>
    public decimal Rating { get; set; }
    
    /// <summary>
    /// Yorum
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Onaylandı mı?
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Admin yanıtı
    /// </summary>
    public string? AdminResponse { get; set; }

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
    public decimal? CleanlinessRating { get; set; }

    /// <summary>
    /// İletişim puanı
    /// </summary>
    public decimal? CommunicationRating { get; set; }

    /// <summary>
    /// Check-in puanı
    /// </summary>
    public decimal? CheckInRating { get; set; }

    /// <summary>
    /// Doğruluk puanı
    /// </summary>
    public decimal? AccuracyRating { get; set; }

    /// <summary>
    /// Konum puanı
    /// </summary>
    public decimal? LocationRating { get; set; }

    /// <summary>
    /// Değer puanı
    /// </summary>
    public decimal? ValueRating { get; set; }
} 
namespace MinimalAirbnb.Application.Reviews.DTOs;

/// <summary>
/// Review DTO'su
/// </summary>
public class ReviewDto
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Property ID'si
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// Property başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;

    /// <summary>
    /// Property fotoğrafı
    /// </summary>
    public string? PropertyPhotoUrl { get; set; }

    /// <summary>
    /// Misafir ID'si
    /// </summary>
    public Guid GuestId { get; set; }

    /// <summary>
    /// Misafir adı
    /// </summary>
    public string GuestName { get; set; } = string.Empty;

    /// <summary>
    /// Misafir fotoğrafı
    /// </summary>
    public string? GuestPhotoUrl { get; set; }

    /// <summary>
    /// Ev sahibi ID'si
    /// </summary>
    public Guid HostId { get; set; }

    /// <summary>
    /// Ev sahibi adı
    /// </summary>
    public string HostName { get; set; } = string.Empty;

    /// <summary>
    /// Rezervasyon ID'si
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
    /// Ev sahibi yanıtı
    /// </summary>
    public string? HostResponse { get; set; }

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

    /// <summary>
    /// Onaylandı mı?
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Reddedildi mi?
    /// </summary>
    public bool IsRejected { get; set; }

    /// <summary>
    /// Red nedeni
    /// </summary>
    public string? RejectionReason { get; set; }

    /// <summary>
    /// Moderasyon yapan kullanıcı ID'si
    /// </summary>
    public Guid? ModeratedByUserId { get; set; }

    /// <summary>
    /// Moderasyon yapan kullanıcı adı
    /// </summary>
    public string? ModeratedByUserName { get; set; }

    /// <summary>
    /// Moderasyon tarihi
    /// </summary>
    public DateTime? ModerationDate { get; set; }

    /// <summary>
    /// Beğeni sayısı
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Beğenmeme sayısı
    /// </summary>
    public int DislikeCount { get; set; }

    /// <summary>
    /// Yanıt sayısı
    /// </summary>
    public int ReplyCount { get; set; }

    /// <summary>
    /// Görüntülenme sayısı
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Paylaşım sayısı
    /// </summary>
    public int ShareCount { get; set; }

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Ev sahibi yanıtı var mı?
    /// </summary>
    public bool HasHostReply { get; set; }

    /// <summary>
    /// Ev sahibi yanıtı
    /// </summary>
    public string? HostReply { get; set; }

    /// <summary>
    /// Ev sahibi yanıt tarihi
    /// </summary>
    public DateTime? HostReplyDate { get; set; }

    /// <summary>
    /// Anonim mi?
    /// </summary>
    public bool IsAnonymous { get; set; }

    /// <summary>
    /// Önerilen mi?
    /// </summary>
    public bool IsRecommended { get; set; }

    /// <summary>
    /// Öne çıkan mı?
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Spam olarak işaretlendi mi?
    /// </summary>
    public bool IsFlaggedAsSpam { get; set; }

    /// <summary>
    /// Uygunsuz içerik olarak işaretlendi mi?
    /// </summary>
    public bool IsFlaggedAsInappropriate { get; set; }

    /// <summary>
    /// Misafir e-posta adresi
    /// </summary>
    public string GuestEmail { get; set; } = string.Empty;

    /// <summary>
    /// Oluşturulma tarihi (alias)
    /// </summary>
    public DateTime CreatedAt => CreatedDate;

    /// <summary>
    /// Güncellenme tarihi (alias)
    /// </summary>
    public DateTime? UpdatedAt => UpdatedDate;

    /// <summary>
    /// Admin yanıtı
    /// </summary>
    public string? AdminResponse { get; set; }

    /// <summary>
    /// Ev adresi
    /// </summary>
    public string PropertyAddress { get; set; } = string.Empty;

    /// <summary>
    /// Ev şehri
    /// </summary>
    public string PropertyCity { get; set; } = string.Empty;

    /// <summary>
    /// Ev fiyatı
    /// </summary>
    public decimal PropertyPrice { get; set; }

    /// <summary>
    /// Ev maksimum misafir sayısı
    /// </summary>
    public int PropertyMaxGuests { get; set; }

    /// <summary>
    /// Ev sahibi e-posta adresi
    /// </summary>
    public string HostEmail { get; set; } = string.Empty;

    /// <summary>
    /// Ev sahibi telefon numarası
    /// </summary>
    public string HostPhone { get; set; } = string.Empty;

    /// <summary>
    /// Misafir oluşturulma tarihi
    /// </summary>
    public DateTime GuestCreatedAt { get; set; }

    /// <summary>
    /// Misafir telefon numarası
    /// </summary>
    public string GuestPhone { get; set; } = string.Empty;
} 
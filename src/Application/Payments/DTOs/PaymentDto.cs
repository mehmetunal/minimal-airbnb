using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Payments.DTOs;

/// <summary>
/// Payment DTO'su
/// </summary>
public class PaymentDto
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Kullanıcı ID'si
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Kullanıcı adı
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Rezervasyon ID'si
    /// </summary>
    public Guid ReservationId { get; set; }

    /// <summary>
    /// Rezervasyon bilgileri
    /// </summary>
    public string ReservationInfo { get; set; } = string.Empty;

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
    /// Ev sahibi fotoğrafı
    /// </summary>
    public string? HostPhotoUrl { get; set; }

    /// <summary>
    /// Ödeme tutarı
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Para birimi
    /// </summary>
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Ödeme tipi
    /// </summary>
    public string PaymentType { get; set; } = string.Empty;

    /// <summary>
    /// Ödeme yöntemi
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Ödeme sağlayıcısı
    /// </summary>
    public PaymentProvider Provider { get; set; }

    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// İşlem ID'si (ödeme sağlayıcısından)
    /// </summary>
    public string? TransactionId { get; set; }

    /// <summary>
    /// Sağlayıcı işlem ID'si
    /// </summary>
    public string? ProviderTransactionId { get; set; }

    /// <summary>
    /// Ödeme açıklaması
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ödeme notları
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Ödeme onaylandı mı?
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Onay tarihi
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

    /// <summary>
    /// Onaylayan kullanıcı ID'si
    /// </summary>
    public Guid? ApprovedByUserId { get; set; }

    /// <summary>
    /// Onaylayan kullanıcı adı
    /// </summary>
    public string? ApprovedByUserName { get; set; }

    /// <summary>
    /// Ödeme iptal edildi mi?
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// İptal tarihi
    /// </summary>
    public DateTime? CancellationDate { get; set; }

    /// <summary>
    /// İptal nedeni
    /// </summary>
    public string? CancellationReason { get; set; }

    /// <summary>
    /// İptal eden kullanıcı ID'si
    /// </summary>
    public Guid? CancelledByUserId { get; set; }

    /// <summary>
    /// İptal eden kullanıcı adı
    /// </summary>
    public string? CancelledByUserName { get; set; }

    /// <summary>
    /// Ödeme iade edildi mi?
    /// </summary>
    public bool IsRefunded { get; set; }

    /// <summary>
    /// İade tarihi
    /// </summary>
    public DateTime? RefundDate { get; set; }

    /// <summary>
    /// İade tutarı
    /// </summary>
    public decimal? RefundAmount { get; set; }

    /// <summary>
    /// İade nedeni
    /// </summary>
    public string? RefundReason { get; set; }

    /// <summary>
    /// İade eden kullanıcı ID'si
    /// </summary>
    public Guid? RefundedByUserId { get; set; }

    /// <summary>
    /// İade eden kullanıcı adı
    /// </summary>
    public string? RefundedByUserName { get; set; }

    /// <summary>
    /// Ödeme başarısız mı?
    /// </summary>
    public bool IsFailed { get; set; }

    /// <summary>
    /// Başarısız olma tarihi
    /// </summary>
    public DateTime? FailedDate { get; set; }

    /// <summary>
    /// Hata mesajı
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Hata kodu
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Ödeme beklemede mi?
    /// </summary>
    public bool IsPending { get; set; }

    /// <summary>
    /// Bekleme nedeni
    /// </summary>
    public string? PendingReason { get; set; }

    /// <summary>
    /// Ödeme süresi doldu mu?
    /// </summary>
    public bool IsExpired { get; set; }

    /// <summary>
    /// Süre dolma tarihi
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Ödeme şifrelenmiş mi?
    /// </summary>
    public bool IsEncrypted { get; set; }

    /// <summary>
    /// Şifreleme anahtarı
    /// </summary>
    public string? EncryptionKey { get; set; }

    /// <summary>
    /// Ödeme güvenli mi?
    /// </summary>
    public bool IsSecure { get; set; }

    /// <summary>
    /// SSL sertifikası var mı?
    /// </summary>
    public bool HasSslCertificate { get; set; }

    /// <summary>
    /// 3D Secure kullanıldı mı?
    /// </summary>
    public bool Is3DSecure { get; set; }

    /// <summary>
    /// Ödeme doğrulandı mı?
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Doğrulama tarihi
    /// </summary>
    public DateTime? VerificationDate { get; set; }

    /// <summary>
    /// Doğrulayan kullanıcı ID'si
    /// </summary>
    public Guid? VerifiedByUserId { get; set; }

    /// <summary>
    /// Doğrulayan kullanıcı adı
    /// </summary>
    public string? VerifiedByUserName { get; set; }

    /// <summary>
    /// Ödeme şüpheli mi?
    /// </summary>
    public bool IsSuspicious { get; set; }

    /// <summary>
    /// Şüpheli işlem nedeni
    /// </summary>
    public string? SuspiciousReason { get; set; }

    /// <summary>
    /// Ödeme bloke edildi mi?
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Bloke etme nedeni
    /// </summary>
    public string? BlockReason { get; set; }

    /// <summary>
    /// Bloke eden kullanıcı ID'si
    /// </summary>
    public Guid? BlockedByUserId { get; set; }

    /// <summary>
    /// Bloke eden kullanıcı adı
    /// </summary>
    public string? BlockedByUserName { get; set; }

    /// <summary>
    /// Ödeme etiketleri
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Ödeme kategorisi
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Ödeme önceliği (1-5)
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// Ödeme gizli mi?
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// Ödeme paylaşıldı mı?
    /// </summary>
    public bool IsShared { get; set; }

    /// <summary>
    /// Ödeme paylaşım linki
    /// </summary>
    public string? ShareLink { get; set; }

    /// <summary>
    /// Ödeme paylaşım tarihi
    /// </summary>
    public DateTime? SharedDate { get; set; }

    /// <summary>
    /// Ödeme görüntülenme sayısı
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Ödeme beğeni sayısı (paylaşıldıysa)
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Ödeme yorum sayısı (paylaşıldıysa)
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// Ödeme kopyalanma sayısı
    /// </summary>
    public int CopyCount { get; set; }

    /// <summary>
    /// Misafir e-posta adresi
    /// </summary>
    public string GuestEmail { get; set; } = string.Empty;

    /// <summary>
    /// Misafir telefon numarası
    /// </summary>
    public string GuestPhone { get; set; } = string.Empty;

    /// <summary>
    /// Misafir oluşturulma tarihi
    /// </summary>
    public DateTime GuestCreatedAt { get; set; }

    /// <summary>
    /// Check-in tarihi
    /// </summary>
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// Check-out tarihi
    /// </summary>
    public DateTime CheckOutDate { get; set; }

    /// <summary>
    /// Toplam fiyat
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Rezervasyon durumu
    /// </summary>
    public ReservationStatus ReservationStatus { get; set; }

    /// <summary>
    /// Ödeme sayısı
    /// </summary>
    public int PaymentCount { get; set; }

    /// <summary>
    /// Toplam ödenen tutar
    /// </summary>
    public decimal TotalPaidAmount { get; set; }

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Değiştirilme tarihi
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Ödeme süresi (oluşturulma tarihinden itibaren)
    /// </summary>
    public TimeSpan Duration => DateTime.UtcNow - CreatedDate;

    /// <summary>
    /// Ödeme yeni mi? (24 saatten yeni)
    /// </summary>
    public bool IsNew => Duration.TotalHours < 24;

    /// <summary>
    /// Ödeme acil mi? (1 saatten yeni ve yüksek öncelik)
    /// </summary>
    public bool IsUrgent => Duration.TotalHours < 1 && Priority >= 4;
} 
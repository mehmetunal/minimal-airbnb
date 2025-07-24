using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Ödeme entity'si
/// </summary>
public class Payment : BaseEntity
{
    /// <summary>
    /// Kullanıcı ID'si
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Rezervasyon ID'si
    /// </summary>
    [Required]
    public Guid ReservationId { get; set; }

    /// <summary>
    /// Ödeme tutarı
    /// </summary>
    [Required]
    [Range(0, 100000)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Ödeme yöntemi
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    /// <summary>
    /// İşlem ID'si (ödeme sağlayıcısından)
    /// </summary>
    [StringLength(100)]
    public string? TransactionId { get; set; }

    /// <summary>
    /// Sağlayıcı işlem ID'si
    /// </summary>
    [StringLength(100)]
    public string? ProviderTransactionId { get; set; }

    /// <summary>
    /// Sağlayıcı referans ID'si
    /// </summary>
    [StringLength(100)]
    public string? ProviderReferenceId { get; set; }

    /// <summary>
    /// Ödeme sağlayıcısı
    /// </summary>
    public PaymentProvider Provider { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime? PaymentDate { get; set; }

    /// <summary>
    /// İade tutarı
    /// </summary>
    [Range(0, 100000)]
    public decimal? RefundAmount { get; set; }

    /// <summary>
    /// İade tarihi
    /// </summary>
    public DateTime? RefundDate { get; set; }

    /// <summary>
    /// İade nedeni
    /// </summary>
    [StringLength(500)]
    public string? RefundReason { get; set; }

    /// <summary>
    /// Hata mesajı
    /// </summary>
    [StringLength(1000)]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Başarılı işlem sayısı
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    /// Son deneme tarihi
    /// </summary>
    public DateTime? LastRetryDate { get; set; }



    /// <summary>
    /// Kart son 4 hanesi
    /// </summary>
    [StringLength(4)]
    public string? LastFourDigits { get; set; }

    /// <summary>
    /// Kart tipi
    /// </summary>
    [StringLength(50)]
    public string? CardType { get; set; }

    /// <summary>
    /// Para birimi
    /// </summary>
    [StringLength(3)]
    public string Currency { get; set; } = "TRY";

    /// <summary>
    /// Döviz kuru
    /// </summary>
    [Range(0, 1000)]
    public decimal? ExchangeRate { get; set; }

    /// <summary>
    /// Orijinal tutar (farklı para biriminde)
    /// </summary>
    [Range(0, 100000)]
    public decimal? OriginalAmount { get; set; }

    /// <summary>
    /// Orijinal para birimi
    /// </summary>
    [StringLength(3)]
    public string? OriginalCurrency { get; set; }

    // Navigation Properties
    /// <summary>
    /// Kullanıcı
    /// </summary>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Rezervasyon
    /// </summary>
    public virtual Reservation Reservation { get; set; } = null!;

    // Computed Properties
    /// <summary>
    /// Ödeme başarılı mı?
    /// </summary>
    [NotMapped]
    public bool IsSuccessful => Status == PaymentStatus.Success;

    /// <summary>
    /// Ödeme başarısız mı?
    /// </summary>
    [NotMapped]
    public bool IsFailed => Status == PaymentStatus.Failed;

    /// <summary>
    /// Ödeme iade edildi mi?
    /// </summary>
    [NotMapped]
    public bool IsRefunded => Status == PaymentStatus.Refunded;

    /// <summary>
    /// Ödeme iptal edildi mi?
    /// </summary>
    [NotMapped]
    public bool IsCancelled => Status == PaymentStatus.Cancelled;

    /// <summary>
    /// Ödeme bekliyor mu?
    /// </summary>
    [NotMapped]
    public bool IsPending => Status == PaymentStatus.Pending || Status == PaymentStatus.Processing;
} 
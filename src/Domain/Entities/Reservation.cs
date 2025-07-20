using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Rezervasyon entity'si
/// </summary>
public class Reservation : BaseEntity
{
    /// <summary>
    /// Misafir ID'si
    /// </summary>
    [Required]
    public Guid GuestId { get; set; }

    /// <summary>
    /// Ev ID'si
    /// </summary>
    [Required]
    public Guid PropertyId { get; set; }

    /// <summary>
    /// Check-in tarihi
    /// </summary>
    [Required]
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// Check-out tarihi
    /// </summary>
    [Required]
    public DateTime CheckOutDate { get; set; }

    /// <summary>
    /// Misafir sayısı
    /// </summary>
    [Range(1, 20)]
    public int GuestCount { get; set; }

    /// <summary>
    /// Toplam gün sayısı
    /// </summary>
    public int TotalDays { get; set; }

    /// <summary>
    /// Günlük fiyat
    /// </summary>
    [Range(0, 10000)]
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// Temizlik ücreti
    /// </summary>
    [Range(0, 1000)]
    public decimal CleaningFee { get; set; }

    /// <summary>
    /// Hizmet ücreti
    /// </summary>
    [Range(0, 1000)]
    public decimal ServiceFee { get; set; }

    /// <summary>
    /// Toplam fiyat
    /// </summary>
    [Range(0, 100000)]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Rezervasyon durumu
    /// </summary>
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    /// <summary>
    /// Özel istekler
    /// </summary>
    [StringLength(1000)]
    public string? SpecialRequests { get; set; }

    /// <summary>
    /// İptal nedeni
    /// </summary>
    [StringLength(500)]
    public string? CancellationReason { get; set; }

    /// <summary>
    /// İptal tarihi
    /// </summary>
    public DateTime? CancellationDate { get; set; }

    /// <summary>
    /// İptal eden kullanıcı ID'si
    /// </summary>
    public Guid? CancelledByUserId { get; set; }

    /// <summary>
    /// Onay tarihi
    /// </summary>
    public DateTime? ConfirmationDate { get; set; }

    /// <summary>
    /// Onaylayan kullanıcı ID'si
    /// </summary>
    public Guid? ConfirmedByUserId { get; set; }

    /// <summary>
    /// Check-in saati
    /// </summary>
    public TimeSpan CheckInTime { get; set; } = new TimeSpan(15, 0, 0); // 15:00

    /// <summary>
    /// Check-out saati
    /// </summary>
    public TimeSpan CheckOutTime { get; set; } = new TimeSpan(11, 0, 0); // 11:00

    /// <summary>
    /// Gerçek check-in tarihi
    /// </summary>
    public DateTime? ActualCheckInDate { get; set; }

    /// <summary>
    /// Gerçek check-out tarihi
    /// </summary>
    public DateTime? ActualCheckOutDate { get; set; }

    // Navigation Properties
    /// <summary>
    /// Misafir
    /// </summary>
    public virtual User Guest { get; set; } = null!;

    /// <summary>
    /// Ev
    /// </summary>
    public virtual Property Property { get; set; } = null!;

    /// <summary>
    /// İptal eden kullanıcı
    /// </summary>
    public virtual User? CancelledByUser { get; set; }

    /// <summary>
    /// Onaylayan kullanıcı
    /// </summary>
    public virtual User? ConfirmedByUser { get; set; }

    /// <summary>
    /// Ödemeler
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    /// <summary>
    /// Yorumlar
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// Mesajlar
    /// </summary>
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    // Computed Properties
    /// <summary>
    /// Rezervasyon aktif mi?
    /// </summary>
    [NotMapped]
    public bool IsActive => Status == ReservationStatus.Confirmed && 
                           CheckInDate <= DateTime.Now && 
                           CheckOutDate >= DateTime.Now;

    /// <summary>
    /// Rezervasyon geçmiş mi?
    /// </summary>
    [NotMapped]
    public bool IsPast => CheckOutDate < DateTime.Now;

    /// <summary>
    /// Rezervasyon gelecekte mi?
    /// </summary>
    [NotMapped]
    public bool IsFuture => CheckInDate > DateTime.Now;

    /// <summary>
    /// Rezervasyon iptal edilebilir mi?
    /// </summary>
    [NotMapped]
    public bool CanBeCancelled => Status == ReservationStatus.Pending || 
                                 Status == ReservationStatus.Confirmed;

    /// <summary>
    /// Günlük toplam fiyat
    /// </summary>
    [NotMapped]
    public decimal DailyTotalPrice => PricePerNight + CleaningFee + ServiceFee;
} 
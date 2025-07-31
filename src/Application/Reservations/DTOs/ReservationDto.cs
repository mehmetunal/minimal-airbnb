using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Reservations.DTOs;

/// <summary>
/// Reservation DTO'su
/// </summary>
public class ReservationDto
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Misafir ID'si
    /// </summary>
    public Guid GuestId { get; set; }

    /// <summary>
    /// Misafir adı
    /// </summary>
    public string GuestName { get; set; } = string.Empty;

    /// <summary>
    /// Ev ID'si
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;

    /// <summary>
    /// Ev fotoğrafı URL'i
    /// </summary>
    public string? PropertyPhotoUrl { get; set; }

    /// <summary>
    /// Ev sahibi ID'si
    /// </summary>
    public Guid HostId { get; set; }

    /// <summary>
    /// Ev sahibi adı
    /// </summary>
    public string HostName { get; set; } = string.Empty;

    /// <summary>
    /// Check-in tarihi
    /// </summary>
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// Check-out tarihi
    /// </summary>
    public DateTime CheckOutDate { get; set; }

    /// <summary>
    /// Misafir sayısı
    /// </summary>
    public int GuestCount { get; set; }

    /// <summary>
    /// Toplam gün sayısı
    /// </summary>
    public int TotalDays { get; set; }

    /// <summary>
    /// Toplam gecelik sayısı
    /// </summary>
    public int TotalNights { get; set; }

    /// <summary>
    /// Günlük fiyat
    /// </summary>
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// Temizlik ücreti
    /// </summary>
    public decimal CleaningFee { get; set; }

    /// <summary>
    /// Hizmet ücreti
    /// </summary>
    public decimal ServiceFee { get; set; }

    /// <summary>
    /// Toplam fiyat
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Rezervasyon durumu
    /// </summary>
    public ReservationStatus Status { get; set; }

    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus PaymentStatus { get; set; }

    /// <summary>
    /// Özel istekler
    /// </summary>
    public string? SpecialRequests { get; set; }

    /// <summary>
    /// İptal nedeni
    /// </summary>
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
    /// İptal eden kullanıcı adı
    /// </summary>
    public string? CancelledByUserName { get; set; }

    /// <summary>
    /// Onay tarihi
    /// </summary>
    public DateTime? ConfirmationDate { get; set; }

    /// <summary>
    /// Onaylayan kullanıcı ID'si
    /// </summary>
    public Guid? ConfirmedByUserId { get; set; }

    /// <summary>
    /// Onaylayan kullanıcı adı
    /// </summary>
    public string? ConfirmedByUserName { get; set; }

    /// <summary>
    /// Check-in saati
    /// </summary>
    public TimeSpan CheckInTime { get; set; }

    /// <summary>
    /// Check-out saati
    /// </summary>
    public TimeSpan CheckOutTime { get; set; }

    /// <summary>
    /// Değerlendirme yapıldı mı?
    /// </summary>
    public bool HasReview { get; set; }

    /// <summary>
    /// Değerlendirme puanı
    /// </summary>
    public decimal? ReviewRating { get; set; }

    /// <summary>
    /// Değerlendirme yorumu
    /// </summary>
    public string? ReviewComment { get; set; }

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Oluşturulma tarihi (alternatif)
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Güncellenme tarihi (alternatif)
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Onaylanma tarihi (alternatif)
    /// </summary>
    public DateTime? ConfirmedDate { get; set; }

    /// <summary>
    /// Misafir e-posta adresi
    /// </summary>
    public string GuestEmail { get; set; } = string.Empty;

    /// <summary>
    /// Misafir telefon numarası
    /// </summary>
    public string GuestPhone { get; set; } = string.Empty;

    /// <summary>
    /// İptal tarihi (alias)
    /// </summary>
    public DateTime? CancelledAt => CancellationDate;

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
} 
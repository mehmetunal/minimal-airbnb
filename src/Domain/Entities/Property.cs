using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Ev/İlan entity'si
/// </summary>
public class Property : BaseEntity
{
    /// <summary>
    /// Başlık
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Açıklama
    /// </summary>
    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev sahibi ID'si
    /// </summary>
    [Required]
    public Guid HostId { get; set; }
    
    /// <summary>
    /// Ev tipi
    /// </summary>
    public PropertyType PropertyType { get; set; }
    
    /// <summary>
    /// Oda sayısı
    /// </summary>
    [Range(1, 20)]
    public int BedroomCount { get; set; }
    
    /// <summary>
    /// Yatak sayısı
    /// </summary>
    [Range(1, 20)]
    public int BedCount { get; set; }
    
    /// <summary>
    /// Banyo sayısı
    /// </summary>
    [Range(1, 10)]
    public int BathroomCount { get; set; }
    
    /// <summary>
    /// Maksimum misafir sayısı
    /// </summary>
    [Range(1, 20)]
    public int MaxGuestCount { get; set; }
    
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
    /// Adres
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Address { get; set; } = string.Empty;
    
    /// <summary>
    /// Şehir
    /// </summary>
    [Required]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;
    
    /// <summary>
    /// Ülke
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Country { get; set; } = string.Empty;
    
    /// <summary>
    /// Posta kodu
    /// </summary>
    [StringLength(20)]
    public string PostalCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Enlem (Latitude)
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Boylam (Longitude)
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// WiFi var mı?
    /// </summary>
    public bool HasWifi { get; set; }

    /// <summary>
    /// Klima var mı?
    /// </summary>
    public bool HasAirConditioning { get; set; }

    /// <summary>
    /// Mutfak var mı?
    /// </summary>
    public bool HasKitchen { get; set; }

    /// <summary>
    /// Otopark var mı?
    /// </summary>
    public bool HasParking { get; set; }

    /// <summary>
    /// Havuz var mı?
    /// </summary>
    public bool HasPool { get; set; }

    /// <summary>
    /// Evcil hayvan kabul ediyor mu?
    /// </summary>
    public bool AllowsPets { get; set; }

    /// <summary>
    /// Sigara içmeye izin veriyor mu?
    /// </summary>
    public bool AllowsSmoking { get; set; }

    /// <summary>
    /// Minimum konaklama süresi (gün)
    /// </summary>
    [Range(1, 30)]
    public int MinimumStayDays { get; set; } = 1;

    /// <summary>
    /// Maksimum konaklama süresi (gün)
    /// </summary>
    [Range(1, 365)]
    public int MaximumStayDays { get; set; } = 30;

    /// <summary>
    /// İptal politikası (gün)
    /// </summary>
    [Range(0, 30)]
    public int CancellationPolicyDays { get; set; } = 24;

    /// <summary>
    /// Ortalama puan
    /// </summary>
    [Range(0, 5)]
    public decimal AverageRating { get; set; }

    /// <summary>
    /// Toplam değerlendirme sayısı
    /// </summary>
    public int TotalReviews { get; set; }

    /// <summary>
    /// Görüntülenme sayısı
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Favori sayısı
    /// </summary>
    public int FavoriteCount { get; set; }

    /// <summary>
    /// Rezervasyon sayısı
    /// </summary>
    public int ReservationCount { get; set; }

    // Navigation Properties
    /// <summary>
    /// Ev sahibi
    /// </summary>
    public virtual User Host { get; set; } = null!;

    /// <summary>
    /// Ev fotoğrafları
    /// </summary>
    public virtual ICollection<PropertyPhoto> Photos { get; set; } = new List<PropertyPhoto>();

    /// <summary>
    /// Rezervasyonlar
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    /// <summary>
    /// Yorumlar
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// Favoriler
    /// </summary>
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    /// <summary>
    /// Mesajlar
    /// </summary>
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    // Computed Properties
    /// <summary>
    /// Toplam fiyat (günlük fiyat + temizlik + hizmet)
    /// </summary>
    [NotMapped]
    public decimal TotalPricePerNight => PricePerNight + CleaningFee + ServiceFee;

    /// <summary>
    /// Tam adres
    /// </summary>
    [NotMapped]
    public string FullAddress => $"{Address}, {City}, {Country} {PostalCode}".Trim();

    /// <summary>
    /// Evin durumu (müsait/meşgul)
    /// </summary>
    [NotMapped]
    public bool IsAvailable => IsPublish && !IsDeleted;
} 
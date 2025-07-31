using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.DTOs;

/// <summary>
/// Property DTO'su
/// </summary>
public class PropertyDto
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Başlık
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Açıklama
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Ev sahibi ID'si
    /// </summary>
    public Guid HostId { get; set; }

    /// <summary>
    /// Ev sahibi adı
    /// </summary>
    public string HostName { get; set; } = string.Empty;

    /// <summary>
    /// Ev tipi
    /// </summary>
    public PropertyType PropertyType { get; set; }

    /// <summary>
    /// Oda sayısı
    /// </summary>
    public int BedroomCount { get; set; }

    /// <summary>
    /// Yatak sayısı
    /// </summary>
    public int BedCount { get; set; }

    /// <summary>
    /// Banyo sayısı
    /// </summary>
    public int BathroomCount { get; set; }

    /// <summary>
    /// Maksimum misafir sayısı
    /// </summary>
    public int MaxGuestCount { get; set; }

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
    public decimal TotalPricePerNight { get; set; }

    /// <summary>
    /// Adres
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Şehir
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Posta kodu
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Tam adres
    /// </summary>
    public string FullAddress { get; set; } = string.Empty;

    /// <summary>
    /// Enlem
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Boylam
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
    /// Evcil hayvan izni var mı?
    /// </summary>
    public bool AllowsPets { get; set; }

    /// <summary>
    /// Sigara izni var mı?
    /// </summary>
    public bool AllowsSmoking { get; set; }

    /// <summary>
    /// Minimum konaklama süresi
    /// </summary>
    public int MinimumStayDays { get; set; }

    /// <summary>
    /// Maksimum konaklama süresi
    /// </summary>
    public int MaximumStayDays { get; set; }

    /// <summary>
    /// İptal politikası (gün)
    /// </summary>
    public int CancellationPolicyDays { get; set; }

    /// <summary>
    /// Ortalama puan
    /// </summary>
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

    /// <summary>
    /// Ana fotoğraf URL'i
    /// </summary>
    public string? MainPhotoUrl { get; set; }

    /// <summary>
    /// Fotoğraf sayısı
    /// </summary>
    public int PhotoCount { get; set; }

    /// <summary>
    /// Müsait mi?
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Fiyat (alias)
    /// </summary>
    public decimal Price => PricePerNight;

    /// <summary>
    /// Yatak odası sayısı (alias)
    /// </summary>
    public int Bedrooms => BedroomCount;

    /// <summary>
    /// Banyo sayısı (alias)
    /// </summary>
    public int Bathrooms => BathroomCount;

    /// <summary>
    /// Maksimum misafir sayısı (alias)
    /// </summary>
    public int MaxGuests => MaxGuestCount;

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Değerlendirme sayısı
    /// </summary>
    public int ReviewCount => TotalReviews;

    /// <summary>
    /// Onaylandı mı?
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Güvenlik kamerası var mı?
    /// </summary>
    public bool HasSecurityCamera { get; set; }

    /// <summary>
    /// Yangın söndürücü var mı?
    /// </summary>
    public bool HasFireExtinguisher { get; set; }

    /// <summary>
    /// İlk yardım çantası var mı?
    /// </summary>
    public bool HasFirstAidKit { get; set; }

    /// <summary>
    /// Yayınlandı mı?
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Ev sahibi e-posta adresi
    /// </summary>
    public string HostEmail { get; set; } = string.Empty;

    /// <summary>
    /// Ev sahibi telefon numarası
    /// </summary>
    public string HostPhone { get; set; } = string.Empty;

    /// <summary>
    /// Oluşturulma tarihi (alias)
    /// </summary>
    public DateTime CreatedDate { get; set; }
} 
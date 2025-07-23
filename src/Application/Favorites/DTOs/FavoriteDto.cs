namespace MinimalAirbnb.Application.Favorites.DTOs;

/// <summary>
/// Favorite DTO'su
/// </summary>
public class FavoriteDto
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
    /// Kullanıcı tam adı
    /// </summary>
    public string UserFullName { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcı fotoğrafı
    /// </summary>
    public string? UserPhotoUrl { get; set; }

    /// <summary>
    /// Property ID'si
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// Property başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;

    /// <summary>
    /// Property açıklaması
    /// </summary>
    public string PropertyDescription { get; set; } = string.Empty;

    /// <summary>
    /// Property fotoğrafı
    /// </summary>
    public string? PropertyPhotoUrl { get; set; }

    /// <summary>
    /// Property ana fotoğrafı
    /// </summary>
    public string? MainPhotoUrl { get; set; }

    /// <summary>
    /// Property fiyatı (gecelik)
    /// </summary>
    public decimal PropertyPricePerNight { get; set; }

    /// <summary>
    /// Property fiyatı (gecelik) - kısa ad
    /// </summary>
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// Property ortalama puanı
    /// </summary>
    public decimal? PropertyAverageRating { get; set; }

    /// <summary>
    /// Property ortalama puanı - kısa ad
    /// </summary>
    public decimal? AverageRating { get; set; }

    /// <summary>
    /// Property toplam değerlendirme sayısı
    /// </summary>
    public int PropertyTotalReviews { get; set; }

    /// <summary>
    /// Property konumu (şehir)
    /// </summary>
    public string PropertyCity { get; set; } = string.Empty;

    /// <summary>
    /// Property konumu (şehir) - kısa ad
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Property konumu (ülke)
    /// </summary>
    public string PropertyCountry { get; set; } = string.Empty;

    /// <summary>
    /// Property tam adresi
    /// </summary>
    public string PropertyFullAddress { get; set; } = string.Empty;

    /// <summary>
    /// Property tipi
    /// </summary>
    public string PropertyType { get; set; } = string.Empty;

    /// <summary>
    /// Property kapasitesi (misafir sayısı)
    /// </summary>
    public int PropertyGuestCapacity { get; set; }

    /// <summary>
    /// Property yatak odası sayısı
    /// </summary>
    public int PropertyBedroomCount { get; set; }

    /// <summary>
    /// Property banyo sayısı
    /// </summary>
    public int PropertyBathroomCount { get; set; }

    /// <summary>
    /// Property yatak sayısı
    /// </summary>
    public int PropertyBedCount { get; set; }

    /// <summary>
    /// Property özellikleri (amenities)
    /// </summary>
    public List<string> PropertyAmenities { get; set; } = new();

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
    /// Ev sahibi süper host mu?
    /// </summary>
    public bool HostIsSuperHost { get; set; }

    /// <summary>
    /// Ev sahibi doğrulanmış mı?
    /// </summary>
    public bool HostIsVerified { get; set; }

    /// <summary>
    /// Favoriye eklenme tarihi
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Favoriye eklenme tarihi - kısa ad
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Favori notu (kullanıcının eklediği not)
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Favori kategorisi
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Favori etiketleri
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Favori önceliği (1-5)
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// Favori gizli mi?
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// Favori paylaşıldı mı?
    /// </summary>
    public bool IsShared { get; set; }

    /// <summary>
    /// Favori paylaşım linki
    /// </summary>
    public string? ShareLink { get; set; }

    /// <summary>
    /// Favori paylaşım tarihi
    /// </summary>
    public DateTime? SharedDate { get; set; }

    /// <summary>
    /// Favori görüntülenme sayısı
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Favori beğeni sayısı (paylaşıldıysa)
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Favori yorum sayısı (paylaşıldıysa)
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// Favori kopyalanma sayısı
    /// </summary>
    public int CopyCount { get; set; }

    /// <summary>
    /// Favori bildirim açık mı?
    /// </summary>
    public bool NotificationsEnabled { get; set; } = true;

    /// <summary>
    /// Fiyat değişikliği bildirimi açık mı?
    /// </summary>
    public bool PriceChangeNotifications { get; set; } = true;

    /// <summary>
    /// Müsaitlik bildirimi açık mı?
    /// </summary>
    public bool AvailabilityNotifications { get; set; } = true;

    /// <summary>
    /// Yeni değerlendirme bildirimi açık mı?
    /// </summary>
    public bool ReviewNotifications { get; set; } = true;
} 
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Users.DTOs;

/// <summary>
/// User DTO'su
/// </summary>
public class UserDto
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Ad
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Soyad
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Tam ad
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcı adı
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Telefon numarası
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Kullanıcı tipi
    /// </summary>
    public UserType UserType { get; set; }

    /// <summary>
    /// Doğum tarihi
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Cinsiyet
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// Biyografi
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// Yaş
    /// </summary>
    public int? Age { get; set; }

    /// <summary>
    /// Şehir
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Ülke
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Adres
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Posta kodu
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Tam adres
    /// </summary>
    public string? FullAddress { get; set; }

    /// <summary>
    /// Profil fotoğrafı URL'i
    /// </summary>
    public string? ProfilePhotoUrl { get; set; }

    /// <summary>
    /// Email onaylandı mı?
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Email doğrulandı mı?
    /// </summary>
    public bool IsEmailVerified { get; set; }

    /// <summary>
    /// Telefon onaylandı mı?
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// İki faktörlü doğrulama aktif mi?
    /// </summary>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Kilitli mi?
    /// </summary>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Kilit son tarihi
    /// </summary>
    public DateTime? LockoutEnd { get; set; }

    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Doğrulanmış mı?
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Son giriş tarihi
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Property sayısı
    /// </summary>
    public int PropertyCount { get; set; }

    /// <summary>
    /// Rezervasyon sayısı
    /// </summary>
    public int ReservationCount { get; set; }

    /// <summary>
    /// Değerlendirme sayısı
    /// </summary>
    public int ReviewCount { get; set; }

    /// <summary>
    /// Favori sayısı
    /// </summary>
    public int FavoriteCount { get; set; }
}
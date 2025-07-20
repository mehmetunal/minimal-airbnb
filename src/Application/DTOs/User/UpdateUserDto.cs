using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.User;

/// <summary>
/// Kullanıcı güncelleme DTO'su
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// Kullanıcı ID'si
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Email adresi
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Kullanıcı adı
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// Ad
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Soyad
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Telefon numarası
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// Doğum tarihi
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Cinsiyet
    /// </summary>
    public Gender? Gender { get; set; }
    
    /// <summary>
    /// Profil fotoğrafı
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
    
    /// <summary>
    /// Adres
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Şehir
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Ülke
    /// </summary>
    public string? Country { get; set; }
    
    /// <summary>
    /// Posta kodu
    /// </summary>
    public string? PostalCode { get; set; }
    
    /// <summary>
    /// Biyografi
    /// </summary>
    public string? Bio { get; set; }
    
    /// <summary>
    /// Kullanıcı tipi
    /// </summary>
    public UserType? UserType { get; set; }
    
    /// <summary>
    /// Doğrulanmış kullanıcı mı?
    /// </summary>
    public bool? IsVerified { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool? IsActive { get; set; }
} 
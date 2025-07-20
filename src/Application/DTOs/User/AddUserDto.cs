using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.User;

/// <summary>
/// Kullanıcı ekleme DTO'su
/// </summary>
public class AddUserDto
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Kullanıcı adı
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    
    /// <summary>
    /// Ad
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Soyad
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Telefon numarası
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Şifre
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Doğum tarihi
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Cinsiyet
    /// </summary>
    public Gender? Gender { get; set; }
    
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
    /// Profil resmi URL'si
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
    
    /// <summary>
    /// Kullanıcı tipi
    /// </summary>
    public UserType UserType { get; set; } = UserType.Guest;
} 
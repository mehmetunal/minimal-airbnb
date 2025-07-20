using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.User;

/// <summary>
/// Kullanıcı sonuç DTO'su
/// </summary>
public class UserResultDto
{
    /// <summary>
    /// Kullanıcı ID'si
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ad
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Soyad
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Telefon numarası
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// Doğum tarihi
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Profil fotoğrafı
    /// </summary>
    public string? ProfilePicture { get; set; }
    
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
    /// Kullanıcı tipi
    /// </summary>
    public UserType UserType { get; set; }
    
    /// <summary>
    /// Doğrulanmış kullanıcı mı?
    /// </summary>
    public bool IsVerified { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime? ModifiedDate { get; set; }
    
    /// <summary>
    /// Tam ad (Ad + Soyad)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}".Trim();
} 
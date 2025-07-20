using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Kullanıcı entity'si
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Ad
    /// </summary>
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Soyad
    /// </summary>
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Profil fotoğrafı URL'i
    /// </summary>
    [StringLength(500)]
    public string? ProfilePicture { get; set; }

    /// <summary>
    /// Doğum tarihi
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Adres
    /// </summary>
    [StringLength(500)]
    public string? Address { get; set; }

    /// <summary>
    /// Şehir
    /// </summary>
    [StringLength(100)]
    public string? City { get; set; }

    /// <summary>
    /// Ülke
    /// </summary>
    [StringLength(100)]
    public string? Country { get; set; }

    /// <summary>
    /// Posta kodu
    /// </summary>
    [StringLength(20)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Kullanıcı tipi
    /// </summary>
    public UserType UserType { get; set; }

    /// <summary>
    /// Hesap doğrulandı mı?
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Hesap aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Son giriş tarihi
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    // Navigation Properties
    /// <summary>
    /// Kullanıcının ilanları
    /// </summary>
    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    /// <summary>
    /// Kullanıcının rezervasyonları
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    /// <summary>
    /// Kullanıcının yorumları
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// Kullanıcının favorileri
    /// </summary>
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    /// <summary>
    /// Kullanıcının gönderdiği mesajlar
    /// </summary>
    public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();

    /// <summary>
    /// Kullanıcının aldığı mesajlar
    /// </summary>
    public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();

    /// <summary>
    /// Kullanıcının ödemeleri
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    // Computed Properties
    /// <summary>
    /// Tam ad
    /// </summary>
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Yaş
    /// </summary>
    [NotMapped]
    public int? Age => DateOfBirth?.Year > 0 ? DateTime.Now.Year - DateOfBirth.Value.Year : null;
} 
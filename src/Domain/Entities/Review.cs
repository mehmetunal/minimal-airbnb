using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Yorum/Değerlendirme entity'si
/// </summary>
public class Review : BaseEntity
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
    /// Rezervasyon ID'si (opsiyonel)
    /// </summary>
    public Guid? ReservationId { get; set; }

    /// <summary>
    /// Puan (1-5)
    /// </summary>
    [Range(1, 5)]
    public int Rating { get; set; }

    /// <summary>
    /// Yorum metni
    /// </summary>
    [Required]
    [StringLength(2000)]
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Ev sahibi yanıtı
    /// </summary>
    [StringLength(2000)]
    public string? HostResponse { get; set; }

    /// <summary>
    /// Ev sahibi yanıt tarihi
    /// </summary>
    public DateTime? HostResponseDate { get; set; }

    /// <summary>
    /// Ev sahibi yanıtlayan kullanıcı ID'si
    /// </summary>
    public Guid? HostResponseUserId { get; set; }

    /// <summary>
    /// Yorum onaylandı mı?
    /// </summary>
    public bool IsApproved { get; set; } = false;

    /// <summary>
    /// Yorum reddedildi mi?
    /// </summary>
    public bool IsRejected { get; set; } = false;

    /// <summary>
    /// Red nedeni
    /// </summary>
    [StringLength(500)]
    public string? RejectionReason { get; set; }

    /// <summary>
    /// Onaylayan/Reddeden kullanıcı ID'si
    /// </summary>
    public Guid? ModeratedByUserId { get; set; }

    /// <summary>
    /// Moderasyon tarihi
    /// </summary>
    public DateTime? ModerationDate { get; set; }

    /// <summary>
    /// Beğeni sayısı
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Beğenmeme sayısı
    /// </summary>
    public int DislikeCount { get; set; }

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
    /// Rezervasyon (opsiyonel)
    /// </summary>
    public virtual Reservation? Reservation { get; set; }

    /// <summary>
    /// Ev sahibi yanıtlayan kullanıcı
    /// </summary>
    public virtual User? HostResponseUser { get; set; }

    /// <summary>
    /// Moderasyon yapan kullanıcı
    /// </summary>
    public virtual User? ModeratedByUser { get; set; }

    // Computed Properties
    /// <summary>
    /// Yorum aktif mi?
    /// </summary>
    [NotMapped]
    public bool IsActive => IsApproved && !IsRejected && IsPublish && !IsDeleted;

    /// <summary>
    /// Yorum beklemede mi?
    /// </summary>
    [NotMapped]
    public bool IsPending => !IsApproved && !IsRejected;

    /// <summary>
    /// Net beğeni sayısı
    /// </summary>
    [NotMapped]
    public int NetLikes => LikeCount - DislikeCount;
} 
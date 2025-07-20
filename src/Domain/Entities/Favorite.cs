using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Favori entity'si
/// </summary>
public class Favorite : BaseEntity
{
    /// <summary>
    /// Kullanıcı ID'si
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Ev ID'si
    /// </summary>
    [Required]
    public Guid PropertyId { get; set; }

    /// <summary>
    /// Favori notu
    /// </summary>
    [StringLength(500)]
    public string? Note { get; set; }

    /// <summary>
    /// Favori kategorisi
    /// </summary>
    [StringLength(100)]
    public string? Category { get; set; }

    /// <summary>
    /// Favori etiketleri (virgülle ayrılmış)
    /// </summary>
    [StringLength(500)]
    public string? Tags { get; set; }

    /// <summary>
    /// Favori sıralama (kullanıcının özel sıralaması)
    /// </summary>
    public int? SortOrder { get; set; }

    /// <summary>
    /// Favori gizli mi?
    /// </summary>
    public bool IsPrivate { get; set; } = false;

    /// <summary>
    /// Favori paylaşıldı mı?
    /// </summary>
    public bool IsShared { get; set; } = false;

    /// <summary>
    /// Paylaşım tarihi
    /// </summary>
    public DateTime? SharedDate { get; set; }

    /// <summary>
    /// Paylaşım linki
    /// </summary>
    [StringLength(500)]
    public string? ShareLink { get; set; }

    /// <summary>
    /// Favori ziyaret sayısı
    /// </summary>
    public int VisitCount { get; set; }

    /// <summary>
    /// Son ziyaret tarihi
    /// </summary>
    public DateTime? LastVisitDate { get; set; }

    /// <summary>
    /// Favori hatırlatma tarihi
    /// </summary>
    public DateTime? ReminderDate { get; set; }

    /// <summary>
    /// Hatırlatma notu
    /// </summary>
    [StringLength(200)]
    public string? ReminderNote { get; set; }

    /// <summary>
    /// Hatırlatma gönderildi mi?
    /// </summary>
    public bool ReminderSent { get; set; } = false;

    // Navigation Properties
    /// <summary>
    /// Kullanıcı
    /// </summary>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Ev
    /// </summary>
    public virtual Property Property { get; set; } = null!;

    // Computed Properties
    /// <summary>
    /// Favori aktif mi?
    /// </summary>
    [NotMapped]
    public bool IsActive => IsPublish && !IsDeleted;

    /// <summary>
    /// Favori yeni mi? (7 günden yeni)
    /// </summary>
    [NotMapped]
    public bool IsNew => CreatedDate > DateTime.UtcNow.AddDays(-7);

    /// <summary>
    /// Favori eski mi? (30 günden eski)
    /// </summary>
    [NotMapped]
    public bool IsOld => CreatedDate < DateTime.UtcNow.AddDays(-30);

    /// <summary>
    /// Hatırlatma zamanı geldi mi?
    /// </summary>
    [NotMapped]
    public bool IsReminderDue => ReminderDate.HasValue && 
                                ReminderDate.Value <= DateTime.UtcNow && 
                                !ReminderSent;

    /// <summary>
    /// Etiket listesi
    /// </summary>
    [NotMapped]
    public string[] TagList => Tags?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(t => t.Trim())
                                   .ToArray() ?? Array.Empty<string>();
} 
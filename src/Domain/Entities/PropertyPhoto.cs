using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Ev fotoğrafı entity'si
/// </summary>
public class PropertyPhoto : BaseEntity
{
    /// <summary>
    /// Ev ID'si
    /// </summary>
    [Required]
    public Guid PropertyId { get; set; }

    /// <summary>
    /// Fotoğraf URL'i
    /// </summary>
    [Required]
    [StringLength(500)]
    public string PhotoUrl { get; set; } = string.Empty;

    /// <summary>
    /// Fotoğraf başlığı
    /// </summary>
    [StringLength(200)]
    public string? Caption { get; set; }

    /// <summary>
    /// Fotoğraf açıklaması
    /// </summary>
    [StringLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Ana fotoğraf mı?
    /// </summary>
    public bool IsMainPhoto { get; set; } = false;

    /// <summary>
    /// Fotoğraf sıralaması
    /// </summary>
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// Dosya adı
    /// </summary>
    [StringLength(200)]
    public string? FileName { get; set; }

    /// <summary>
    /// Dosya tipi
    /// </summary>
    [StringLength(50)]
    public string? FileType { get; set; }

    /// <summary>
    /// Dosya boyutu (byte)
    /// </summary>
    public long? FileSize { get; set; }

    /// <summary>
    /// Fotoğraf genişliği (piksel)
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Fotoğraf yüksekliği (piksel)
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// Fotoğraf çözünürlüğü (DPI)
    /// </summary>
    public int? Resolution { get; set; }

    /// <summary>
    /// Fotoğraf kalitesi (1-100)
    /// </summary>
    [Range(1, 100)]
    public int? Quality { get; set; }

    /// <summary>
    /// Fotoğraf etiketleri (virgülle ayrılmış)
    /// </summary>
    [StringLength(500)]
    public string? Tags { get; set; }

    /// <summary>
    /// Fotoğraf kategorisi
    /// </summary>
    [StringLength(100)]
    public string? Category { get; set; }

    /// <summary>
    /// Fotoğraf onaylandı mı?
    /// </summary>
    public bool IsApproved { get; set; } = false;

    /// <summary>
    /// Fotoğraf reddedildi mi?
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
    /// Fotoğraf görüntülenme sayısı
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Fotoğraf beğeni sayısı
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Fotoğraf yorum sayısı
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// Fotoğraf paylaşım sayısı
    /// </summary>
    public int ShareCount { get; set; }

    /// <summary>
    /// Fotoğraf indirme sayısı
    /// </summary>
    public int DownloadCount { get; set; }

    // Navigation Properties
    /// <summary>
    /// Ev
    /// </summary>
    public virtual Property Property { get; set; } = null!;

    /// <summary>
    /// Moderasyon yapan kullanıcı
    /// </summary>
    public virtual User? ModeratedByUser { get; set; }

    // Computed Properties
    /// <summary>
    /// Fotoğraf aktif mi?
    /// </summary>
    [NotMapped]
    public bool IsActive => IsApproved && !IsRejected && IsPublish && !IsDeleted;

    /// <summary>
    /// Fotoğraf beklemede mi?
    /// </summary>
    [NotMapped]
    public bool IsPending => !IsApproved && !IsRejected;

    /// <summary>
    /// Fotoğraf boyutları
    /// </summary>
    [NotMapped]
    public string? Dimensions => Width.HasValue && Height.HasValue 
        ? $"{Width}x{Height}" 
        : null;

    /// <summary>
    /// Dosya boyutu (MB)
    /// </summary>
    [NotMapped]
    public decimal? FileSizeMB => FileSize.HasValue 
        ? Math.Round((decimal)FileSize.Value / (1024 * 1024), 2) 
        : null;

    /// <summary>
    /// Etiket listesi
    /// </summary>
    [NotMapped]
    public string[] TagList => Tags?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(t => t.Trim())
                                   .ToArray() ?? Array.Empty<string>();

    /// <summary>
    /// Fotoğraf popüler mi?
    /// </summary>
    [NotMapped]
    public bool IsPopular => ViewCount > 100 || LikeCount > 10;

    /// <summary>
    /// Fotoğraf yüksek kaliteli mi?
    /// </summary>
    [NotMapped]
    public bool IsHighQuality => Quality.HasValue && Quality.Value >= 80;
} 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Mesaj entity'si
/// </summary>
public class Message : BaseEntity
{
    /// <summary>
    /// Gönderen kullanıcı ID'si
    /// </summary>
    [Required]
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Alıcı kullanıcı ID'si
    /// </summary>
    [Required]
    public Guid ReceiverId { get; set; }
    
    /// <summary>
    /// Rezervasyon ID'si (opsiyonel)
    /// </summary>
    public Guid? ReservationId { get; set; }
    
    /// <summary>
    /// Ev ID'si (opsiyonel)
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// Mesaj konusu
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Subject { get; set; } = string.Empty;
    
    /// <summary>
    /// Mesaj içeriği
    /// </summary>
    [Required]
    [StringLength(2000)]
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Okundu mu?
    /// </summary>
    public bool IsRead { get; set; } = false;
    
    /// <summary>
    /// Okunma tarihi
    /// </summary>
    public DateTime? ReadDate { get; set; }
    
    /// <summary>
    /// Yanıtlanan mesaj ID'si (opsiyonel)
    /// </summary>
    public Guid? ReplyToMessageId { get; set; }

    /// <summary>
    /// Mesaj tipi
    /// </summary>
    public MessageType MessageType { get; set; } = MessageType.Private;

    /// <summary>
    /// Öncelik seviyesi
    /// </summary>
    public MessagePriority Priority { get; set; } = MessagePriority.Normal;

    /// <summary>
    /// Mesaj kategorisi
    /// </summary>
    public MessageCategory Category { get; set; } = MessageCategory.General;

    /// <summary>
    /// Ek dosya URL'i
    /// </summary>
    [StringLength(500)]
    public string? AttachmentUrl { get; set; }

    /// <summary>
    /// Ek dosya adı
    /// </summary>
    [StringLength(200)]
    public string? AttachmentName { get; set; }

    /// <summary>
    /// Ek dosya boyutu (byte)
    /// </summary>
    public long? AttachmentSize { get; set; }

    /// <summary>
    /// Ek dosya tipi
    /// </summary>
    [StringLength(100)]
    public string? AttachmentType { get; set; }

    /// <summary>
    /// Mesaj şifrelenmiş mi?
    /// </summary>
    public bool IsEncrypted { get; set; } = false;

    /// <summary>
    /// Mesaj arşivlendi mi?
    /// </summary>
    public bool IsArchived { get; set; } = false;

    /// <summary>
    /// Arşivlenme tarihi
    /// </summary>
    public DateTime? ArchivedDate { get; set; }

    /// <summary>
    /// Arşivleyen kullanıcı ID'si
    /// </summary>
    public Guid? ArchivedByUserId { get; set; }

    // Navigation Properties
    /// <summary>
    /// Gönderen kullanıcı
    /// </summary>
    public virtual User Sender { get; set; } = null!;
    
    /// <summary>
    /// Alıcı kullanıcı
    /// </summary>
    public virtual User Receiver { get; set; } = null!;
    
    /// <summary>
    /// Rezervasyon (opsiyonel)
    /// </summary>
    public virtual Reservation? Reservation { get; set; }
    
    /// <summary>
    /// Ev (opsiyonel)
    /// </summary>
    public virtual Property? Property { get; set; }
    
    /// <summary>
    /// Yanıtlanan mesaj (opsiyonel)
    /// </summary>
    public virtual Message? ReplyToMessage { get; set; }

    /// <summary>
    /// Bu mesaja gelen yanıtlar
    /// </summary>
    public virtual ICollection<Message> Replies { get; set; } = new List<Message>();

    /// <summary>
    /// Arşivleyen kullanıcı
    /// </summary>
    public virtual User? ArchivedByUser { get; set; }

    // Computed Properties
    /// <summary>
    /// Mesaj yanıt mı?
    /// </summary>
    [NotMapped]
    public bool IsReply => ReplyToMessageId.HasValue;

    /// <summary>
    /// Mesajın yanıtı var mı?
    /// </summary>
    [NotMapped]
    public bool HasReplies => Replies.Any();

    /// <summary>
    /// Mesaj yeni mi?
    /// </summary>
    [NotMapped]
    public bool IsNew => !IsRead;

    /// <summary>
    /// Mesaj eski mi? (7 günden eski)
    /// </summary>
    [NotMapped]
    public bool IsOld => CreatedDate < DateTime.UtcNow.AddDays(-7);

    /// <summary>
    /// Mesaj acil mi?
    /// </summary>
    [NotMapped]
    public bool IsUrgent => Priority == MessagePriority.High || Priority == MessagePriority.Critical;
} 
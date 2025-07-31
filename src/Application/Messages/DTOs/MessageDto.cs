using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Messages.DTOs;

/// <summary>
/// Message DTO'su
/// </summary>
public class MessageDto
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gönderen kullanıcı ID'si
    /// </summary>
    public Guid SenderId { get; set; }

    /// <summary>
    /// Gönderen kullanıcı adı
    /// </summary>
    public string SenderName { get; set; } = string.Empty;

    /// <summary>
    /// Gönderen e-posta adresi
    /// </summary>
    public string SenderEmail { get; set; } = string.Empty;

    /// <summary>
    /// Gönderen profil fotoğrafı
    /// </summary>
    public string? SenderPhotoUrl { get; set; }

    /// <summary>
    /// Alıcı kullanıcı ID'si
    /// </summary>
    public Guid ReceiverId { get; set; }

    /// <summary>
    /// Alıcı kullanıcı adı
    /// </summary>
    public string ReceiverName { get; set; } = string.Empty;

    /// <summary>
    /// Alıcı e-posta adresi
    /// </summary>
    public string ReceiverEmail { get; set; } = string.Empty;

    /// <summary>
    /// Alıcı profil fotoğrafı
    /// </summary>
    public string? ReceiverPhotoUrl { get; set; }

    /// <summary>
    /// Rezervasyon ID'si (opsiyonel)
    /// </summary>
    public Guid? ReservationId { get; set; }

    /// <summary>
    /// Ev ID'si (opsiyonel)
    /// </summary>
    public Guid? PropertyId { get; set; }

    /// <summary>
    /// Ev başlığı (opsiyonel)
    /// </summary>
    public string? PropertyTitle { get; set; }

    /// <summary>
    /// Mesaj konusu
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Mesaj içeriği
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Okundu mu?
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Arşivlendi mi?
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Mesaj önceliği
    /// </summary>
    public MessagePriority Priority { get; set; }

    /// <summary>
    /// Mesaj kategorisi
    /// </summary>
    public MessageCategory Category { get; set; }

    /// <summary>
    /// Toplam mesaj sayısı
    /// </summary>
    public int MessageCount { get; set; }

    /// <summary>
    /// Okunmamış mesaj sayısı
    /// </summary>
    public int UnreadCount { get; set; }

    /// <summary>
    /// Görüntülenme sayısı
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Yanıt sayısı
    /// </summary>
    public int ReplyCount { get; set; }

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

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
    public MessageType MessageType { get; set; }

    /// <summary>
    /// Ek dosya URL'i
    /// </summary>
    public string? AttachmentUrl { get; set; }

    /// <summary>
    /// Oluşturulma tarihi (alias)
    /// </summary>
    public DateTime CreatedDate => CreatedAt;
} 
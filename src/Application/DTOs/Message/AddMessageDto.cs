using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Message;

/// <summary>
/// Mesaj Ekleme DTO
/// </summary>
public class AddMessageDto
{
    /// <summary>
    /// Gönderen kullanıcı ID
    /// </summary>
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Alıcı kullanıcı ID
    /// </summary>
    public Guid ReceiverId { get; set; }
    
    /// <summary>
    /// Mesaj başlığı
    /// </summary>
    public string Subject { get; set; } = string.Empty;
    
    /// <summary>
    /// Mesaj içeriği
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Rezervasyon ID (opsiyonel)
    /// </summary>
    public Guid? ReservationId { get; set; }
    
    /// <summary>
    /// Ev ID (opsiyonel)
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// Okundu mu?
    /// </summary>
    public bool IsRead { get; set; } = false;

    /// <summary>
    /// Mesaj önceliği
    /// </summary>
    public MessagePriority Priority { get; set; }

    /// <summary>
    /// Mesaj kategorisi
    /// </summary>
    public MessageCategory Category { get; set; }
} 
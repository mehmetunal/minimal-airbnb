namespace MinimalAirbnb.Application.DTOs.Message;

/// <summary>
/// Mesaj Sonuç DTO
/// </summary>
public class MessageResultDto
{
    /// <summary>
    /// Mesaj ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gönderen kullanıcı ID
    /// </summary>
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Gönderen kullanıcı adı
    /// </summary>
    public string SenderName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gönderen profil fotoğrafı
    /// </summary>
    public string? SenderPhotoUrl { get; set; }
    
    /// <summary>
    /// Alıcı kullanıcı ID
    /// </summary>
    public Guid ReceiverId { get; set; }
    
    /// <summary>
    /// Alıcı kullanıcı adı
    /// </summary>
    public string ReceiverName { get; set; } = string.Empty;
    
    /// <summary>
    /// Alıcı profil fotoğrafı
    /// </summary>
    public string? ReceiverPhotoUrl { get; set; }
    
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
    /// Ev başlığı (opsiyonel)
    /// </summary>
    public string? PropertyTitle { get; set; }
    
    /// <summary>
    /// Okundu mu?
    /// </summary>
    public bool IsRead { get; set; }
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime UpdatedAt { get; set; }
} 
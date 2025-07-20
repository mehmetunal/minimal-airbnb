namespace MinimalAirbnb.Application.DTOs.Message;

/// <summary>
/// Mesaj Liste DTO
/// </summary>
public class MessageListDto
{
    /// <summary>
    /// Mesaj ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gönderen kullanıcı adı
    /// </summary>
    public string SenderName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gönderen profil fotoğrafı
    /// </summary>
    public string? SenderPhotoUrl { get; set; }
    
    /// <summary>
    /// Alıcı kullanıcı adı
    /// </summary>
    public string ReceiverName { get; set; } = string.Empty;
    
    /// <summary>
    /// Mesaj başlığı
    /// </summary>
    public string Subject { get; set; } = string.Empty;
    
    /// <summary>
    /// Mesaj içeriği (kısaltılmış)
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
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
} 
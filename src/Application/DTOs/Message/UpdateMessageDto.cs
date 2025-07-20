namespace MinimalAirbnb.Application.DTOs.Message;

/// <summary>
/// Mesaj Güncelleme DTO
/// </summary>
public class UpdateMessageDto
{
    /// <summary>
    /// Mesaj ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Mesaj başlığı
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
} 
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Message Repository Interface
/// </summary>
public interface IMessageRepository
{
    /// <summary>
    /// Mesaj ekler
    /// </summary>
    Task<Message> AddAsync(Message message);

    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <returns>Kaydedilen değişiklik sayısı</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// ID'ye göre mesaj getir
    /// </summary>
    Task<Message?> GetByIdAsync(Guid id);

    /// <summary>
    /// Gönderen ID'sine göre mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetBySenderIdAsync(Guid senderId);

    /// <summary>
    /// Alıcı ID'sine göre mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetByReceiverIdAsync(Guid receiverId);

    /// <summary>
    /// İki kullanıcı arasındaki mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetConversationAsync(Guid user1Id, Guid user2Id);

    /// <summary>
    /// Okunmamış mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetUnreadMessagesAsync(Guid userId);

    /// <summary>
    /// Mesajı okundu olarak işaretle
    /// </summary>
    Task MarkAsReadAsync(Guid messageId);

    /// <summary>
    /// Tüm mesajları okundu olarak işaretle
    /// </summary>
    Task MarkAllAsReadAsync(Guid userId, Guid senderId);
} 
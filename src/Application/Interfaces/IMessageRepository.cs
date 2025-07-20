using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Message Repository Interface
/// </summary>
public interface IMessageRepository
{
    /// <summary>
    /// Gönderen ID'sine göre mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetBySenderIdAsync(Guid senderId);
    
    /// <summary>
    /// Alıcı ID'sine göre mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetByReceiverIdAsync(Guid receiverId);
    
    /// <summary>
    /// İki kullanıcı arasındaki konuşmayı getir
    /// </summary>
    Task<IEnumerable<Message>> GetConversationAsync(Guid user1Id, Guid user2Id);
    
    /// <summary>
    /// Okunmamış mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetUnreadMessagesAsync(Guid userId);
    
    /// <summary>
    /// Rezervasyon ID'sine göre mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetByReservationIdAsync(Guid reservationId);
    
    /// <summary>
    /// Ev ID'sine göre mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetByPropertyIdAsync(Guid propertyId);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
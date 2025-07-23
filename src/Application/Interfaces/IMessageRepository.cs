using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Message Repository Interface
/// </summary>
public interface IMessageRepository
{
    /// <summary>
    /// Tüm mesajları getir (IQueryable)
    /// </summary>
    IQueryable<Message> GetAll();

    /// <summary>
    /// Tüm mesajları getir
    /// </summary>
    Task<IEnumerable<Message>> GetAllAsync();

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
    /// Mesaj siler
    /// </summary>
    Task DeleteAsync(Guid id);
} 
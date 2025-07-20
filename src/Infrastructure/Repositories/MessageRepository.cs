using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Mesaj Repository Implementasyonu
/// </summary>
public class MessageRepository : IMessageRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public MessageRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Message>> GetBySenderIdAsync(Guid senderId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Property)
            .Include(m => m.Reservation)
            .Where(m => m.SenderId == senderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetByReceiverIdAsync(Guid receiverId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Property)
            .Include(m => m.Reservation)
            .Where(m => m.ReceiverId == receiverId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetConversationAsync(Guid user1Id, Guid user2Id)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Property)
            .Include(m => m.Reservation)
            .Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                       (m.SenderId == user2Id && m.ReceiverId == user1Id))
            .OrderBy(m => m.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetUnreadMessagesAsync(Guid userId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Property)
            .Include(m => m.Reservation)
            .Where(m => m.ReceiverId == userId && !m.IsRead)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetByReservationIdAsync(Guid reservationId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Property)
            .Include(m => m.Reservation)
            .Where(m => m.ReservationId == reservationId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Include(m => m.Property)
            .Include(m => m.Reservation)
            .Where(m => m.PropertyId == propertyId)
            .ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 
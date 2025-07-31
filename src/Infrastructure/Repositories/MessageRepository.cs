using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Message Repository Implementation
/// </summary>
public class MessageRepository : IMessageRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public MessageRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public IQueryable<Message> GetAll()
    {
        return _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver);
    }

    public async Task<IEnumerable<Message>> GetAllAsync()
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .ToListAsync();
    }

    public async Task<Message> AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        return message;
    }

    public async Task<Message> UpdateAsync(Message message)
    {
        _context.Messages.Update(message);
        return message;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Message>> GetBySenderIdAsync(Guid senderId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Where(m => m.SenderId == senderId)
            .OrderByDescending(m => m.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetByReceiverIdAsync(Guid receiverId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Where(m => m.ReceiverId == receiverId)
            .OrderByDescending(m => m.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetConversationAsync(Guid user1Id, Guid user2Id)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
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
            .Where(m => m.ReceiverId == userId && !m.IsRead)
            .OrderByDescending(m => m.CreatedDate)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(Guid messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message != null)
        {
            message.IsRead = true;
            message.ReadDate = DateTime.UtcNow;
            _context.Messages.Update(message);
        }
    }

    public async Task MarkAllAsReadAsync(Guid userId, Guid senderId)
    {
        var unreadMessages = await _context.Messages
            .Where(m => m.ReceiverId == userId && m.SenderId == senderId && !m.IsRead)
            .ToListAsync();

        foreach (var message in unreadMessages)
        {
            message.IsRead = true;
            message.ReadDate = DateTime.UtcNow;
        }

        _context.Messages.UpdateRange(unreadMessages);
    }

    public async Task DeleteAsync(Guid id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message != null)
        {
            _context.Messages.Remove(message);
        }
    }
} 
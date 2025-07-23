using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Review Repository Implementation
/// </summary>
public class ReviewRepository : IReviewRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public ReviewRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tüm review'ları getir (IQueryable)
    /// </summary>
    public IQueryable<Review> GetAll()
    {
        return _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Include(r => r.HostResponseUser)
            .Include(r => r.ModeratedByUser);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Include(r => r.HostResponseUser)
            .Include(r => r.ModeratedByUser)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(Guid id)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Include(r => r.HostResponseUser)
            .Include(r => r.ModeratedByUser)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Property'ye göre review'ları getir
    /// </summary>
    public async Task<IEnumerable<Review>> GetByPropertyAsync(Guid propertyId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();
    }

    /// <summary>
    /// Kullanıcıya göre review'ları getir
    /// </summary>
    public async Task<IEnumerable<Review>> GetByUserAsync(Guid userId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.GuestId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Property ve kullanıcıya göre review getir
    /// </summary>
    public async Task<Review?> GetByPropertyAndUserAsync(Guid propertyId, Guid userId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .FirstOrDefaultAsync(r => r.PropertyId == propertyId && r.GuestId == userId);
    }

    public async Task<Review> AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        return review;
    }

    public async Task<Review> UpdateAsync(Review review)
    {
        _context.Reviews.Update(review);
        return review;
    }

    public async Task DeleteAsync(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // Additional methods for specific queries
    public async Task<IEnumerable<Review>> GetByGuestIdAsync(Guid guestId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.GuestId == guestId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByReservationIdAsync(Guid reservationId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.ReservationId == reservationId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetApprovedReviewsAsync()
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.IsApproved)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetPendingReviewsAsync()
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => !r.IsApproved && !r.IsRejected)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetRejectedReviewsAsync()
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.IsRejected)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByRatingAsync(int rating)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.Rating == rating)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByRatingRangeAsync(int minRating, int maxRating)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.Rating >= minRating && r.Rating <= maxRating)
            .ToListAsync();
    }

    public async Task<double> GetAverageRatingByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Reviews
            .Where(r => r.PropertyId == propertyId && r.IsApproved)
            .AverageAsync(r => r.Rating);
    }

    public async Task<int> GetReviewCountByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Reviews
            .Where(r => r.PropertyId == propertyId && r.IsApproved)
            .CountAsync();
    }

    public async Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Reservation)
            .Where(r => r.GuestId == userId)
            .ToListAsync();
    }
} 
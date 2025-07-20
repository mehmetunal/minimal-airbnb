using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Yorum Repository Implementasyonu
/// </summary>
public class ReviewRepository : IReviewRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public ReviewRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<Review?> GetByIdAsync(Guid id)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .ToListAsync();
    }

    public async Task<Review> AddAsync(Review entity)
    {
        await _context.Reviews.AddAsync(entity);
        return entity;
    }

    public async Task<Review> UpdateAsync(Review entity)
    {
        _context.Reviews.Update(entity);
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
        }
    }

    public async Task<IEnumerable<Review>> GetByGuestIdAsync(Guid guestId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.GuestId == guestId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByHostIdAsync(Guid hostId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.Property.HostId == hostId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByRatingAsync(int rating)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.Rating == rating)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByRatingRangeAsync(int minRating, int maxRating)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.Rating >= minRating && r.Rating <= maxRating)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByReservationIdAsync(Guid reservationId)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.ReservationId == reservationId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetRecentReviewsAsync(int take = 10)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .OrderByDescending(r => r.CreatedDate)
            .Take(take)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetPositiveReviewsAsync(int minRating = 4)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.Rating >= minRating)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetNegativeReviewsAsync(int maxRating = 2)
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => r.Rating <= maxRating)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetReviewsWithHostResponseAsync()
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => !string.IsNullOrEmpty(r.HostResponse))
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetReviewsWithoutHostResponseAsync()
    {
        return await _context.Reviews
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reservation)
            .Where(r => string.IsNullOrEmpty(r.HostResponse))
            .ToListAsync();
    }

    public async Task<decimal> GetAverageRatingByPropertyAsync(Guid propertyId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();

        if (!reviews.Any())
            return 0;

        return (decimal)reviews.Average(r => r.Rating);
    }

    public async Task<int> GetReviewCountByPropertyAsync(Guid propertyId)
    {
        return await _context.Reviews
            .CountAsync(r => r.PropertyId == propertyId);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Reviews.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsByReservationAsync(Guid reservationId)
    {
        return await _context.Reviews.AnyAsync(r => r.ReservationId == reservationId);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 
using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Reservation Repository Implementation
/// </summary>
public class ReservationRepository : IReservationRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public ReservationRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tüm reservation'ları getir (IQueryable)
    /// </summary>
    public IQueryable<Reservation> GetAll()
    {
        return _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments);
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Property'ye göre reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetByPropertyAsync(Guid propertyId)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();
    }

    /// <summary>
    /// Kullanıcıya göre reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetByUserAsync(Guid userId)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.GuestId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Tarih aralığına göre reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.CheckInDate >= startDate && r.CheckOutDate <= endDate)
            .ToListAsync();
    }

    /// <summary>
    /// Property ve tarih aralığına göre çakışan reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetConflictingReservationsAsync(Guid propertyId, DateTime checkInDate, DateTime checkOutDate)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.PropertyId == propertyId &&
                       r.Status == ReservationStatus.Confirmed &&
                       ((r.CheckInDate <= checkInDate && r.CheckOutDate > checkInDate) ||
                        (r.CheckInDate < checkOutDate && r.CheckOutDate >= checkOutDate) ||
                        (r.CheckInDate >= checkInDate && r.CheckOutDate <= checkOutDate)))
            .ToListAsync();
    }

    /// <summary>
    /// Duruma göre reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus status)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.Status == status)
            .ToListAsync();
    }

    /// <summary>
    /// Aktif reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetActiveReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.Status == ReservationStatus.Confirmed &&
                       r.CheckInDate <= DateTime.UtcNow &&
                       r.CheckOutDate >= DateTime.UtcNow)
            .ToListAsync();
    }

    /// <summary>
    /// Geçmiş reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetPastReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.CheckOutDate < DateTime.UtcNow)
            .ToListAsync();
    }

    /// <summary>
    /// Gelecek reservation'ları getir
    /// </summary>
    public async Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Payments)
            .Where(r => r.CheckInDate > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        return reservation;
    }

    public async Task<Reservation> UpdateAsync(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        return reservation;
    }

    public async Task DeleteAsync(Guid id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 
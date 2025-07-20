using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Rezervasyon Repository Implementasyonu
/// </summary>
public class ReservationRepository : IReservationRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public ReservationRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
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

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reservation>> GetByGuestIdAsync(Guid guestId)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.GuestId == guestId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByHostIdAsync(Guid hostId)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.Property.HostId == hostId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByStatusAsync(Domain.Enums.ReservationStatus status)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => (r.CheckInDate >= startDate && r.CheckInDate <= endDate) ||
                       (r.CheckOutDate >= startDate && r.CheckOutDate <= endDate) ||
                       (r.CheckInDate <= startDate && r.CheckOutDate >= endDate))
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetPendingReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.Status == Domain.Enums.ReservationStatus.Pending)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetConfirmedReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.Status == Domain.Enums.ReservationStatus.Confirmed)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetCancelledReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.Status == Domain.Enums.ReservationStatus.Cancelled)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetCompletedReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Property)
            .Include(r => r.Property.Host)
            .Include(r => r.Reviews)
            .Include(r => r.Payments)
            .Include(r => r.Messages)
            .Include(r => r.CancelledByUser)
            .Include(r => r.ConfirmedByUser)
            .Where(r => r.Status == Domain.Enums.ReservationStatus.Completed)
            .ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 
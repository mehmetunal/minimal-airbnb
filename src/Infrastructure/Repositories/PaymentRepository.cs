using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Ödeme Repository Implementasyonu
/// </summary>
public class PaymentRepository : IPaymentRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public PaymentRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        return payment;
    }

    public async Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .Include(p => p.Reservation.Guest)
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByReservationIdAsync(Guid reservationId)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .Include(p => p.Reservation.Guest)
            .Where(p => p.ReservationId == reservationId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByStatusAsync(Domain.Enums.PaymentStatus status)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .Include(p => p.Reservation.Guest)
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 
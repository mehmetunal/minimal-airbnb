using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Payment Repository Implementation
/// </summary>
public class PaymentRepository : IPaymentRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public PaymentRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tüm payment'ları getir (IQueryable)
    /// </summary>
    public IQueryable<Payment> GetAll()
    {
        return _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Reservation'a göre payment getir
    /// </summary>
    public async Task<Payment?> GetByReservationAsync(Guid reservationId)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
    }

    /// <summary>
    /// Kullanıcıya göre payment'ları getir
    /// </summary>
    public async Task<IEnumerable<Payment>> GetByUserAsync(Guid userId)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Duruma göre payment'ları getir
    /// </summary>
    public async Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    /// <summary>
    /// Ödeme yöntemine göre payment'ları getir
    /// </summary>
    public async Task<IEnumerable<Payment>> GetByPaymentMethodAsync(PaymentMethod paymentMethod)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .Where(p => p.PaymentMethod == paymentMethod)
            .ToListAsync();
    }

    /// <summary>
    /// Tarih aralığına göre payment'ları getir
    /// </summary>
    public async Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Payments
            .Include(p => p.User)
            .Include(p => p.Reservation)
            .Include(p => p.Reservation.Property)
            .Where(p => p.CreatedDate >= startDate && p.CreatedDate <= endDate)
            .ToListAsync();
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        return payment;
    }

    public async Task<Payment> UpdateAsync(Payment payment)
    {
        _context.Payments.Update(payment);
        return payment;
    }

    public async Task DeleteAsync(Guid id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment != null)
        {
            _context.Payments.Remove(payment);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 
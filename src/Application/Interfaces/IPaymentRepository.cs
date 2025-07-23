using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Payment Repository Interface
/// </summary>
public interface IPaymentRepository
{
    /// <summary>
    /// Tüm payment'ları getir (IQueryable)
    /// </summary>
    IQueryable<Payment> GetAll();

    /// <summary>
    /// Tüm payment'ları getir
    /// </summary>
    Task<IEnumerable<Payment>> GetAllAsync();

    /// <summary>
    /// ID'ye göre payment getir
    /// </summary>
    Task<Payment?> GetByIdAsync(Guid id);

    /// <summary>
    /// Reservation'a göre payment getir
    /// </summary>
    Task<Payment?> GetByReservationAsync(Guid reservationId);

    /// <summary>
    /// Kullanıcıya göre payment'ları getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByUserAsync(Guid userId);

    /// <summary>
    /// Duruma göre payment'ları getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status);

    /// <summary>
    /// Ödeme yöntemine göre payment'ları getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByPaymentMethodAsync(PaymentMethod paymentMethod);

    /// <summary>
    /// Tarih aralığına göre payment'ları getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Payment ekle
    /// </summary>
    Task<Payment> AddAsync(Payment payment);

    /// <summary>
    /// Payment güncelle
    /// </summary>
    Task<Payment> UpdateAsync(Payment payment);

    /// <summary>
    /// Payment sil
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
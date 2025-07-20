using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Payment Repository Interface
/// </summary>
public interface IPaymentRepository
{
    /// <summary>
    /// Ödeme ekler
    /// </summary>
    Task<Payment> AddAsync(Payment payment);

    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <returns>Kaydedilen değişiklik sayısı</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Rezervasyon ID'sine göre ödemeleri getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByReservationIdAsync(Guid reservationId);

    /// <summary>
    /// Kullanıcı ID'sine göre ödemeleri getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Duruma göre ödemeleri getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status);
} 
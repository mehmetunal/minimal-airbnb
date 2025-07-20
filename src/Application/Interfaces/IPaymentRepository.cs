using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Payment Repository Interface
/// </summary>
public interface IPaymentRepository
{
    /// <summary>
    /// Kullanıcı ID'sine göre ödemeleri getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId);
    
    /// <summary>
    /// Rezervasyon ID'sine göre ödemeleri getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByReservationIdAsync(Guid reservationId);
    
    /// <summary>
    /// Duruma göre ödemeleri getir
    /// </summary>
    Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
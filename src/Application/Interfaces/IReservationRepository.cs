using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Reservation Repository Interface
/// </summary>
public interface IReservationRepository
{
    /// <summary>
    /// Misafir ID'sine göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByGuestIdAsync(Guid guestId);
    
    /// <summary>
    /// Ev ID'sine göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByPropertyIdAsync(Guid propertyId);
    
    /// <summary>
    /// Duruma göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus status);
    
    /// <summary>
    /// Tarih aralığına göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Çakışan rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetConflictingReservationsAsync(Guid propertyId, DateTime checkInDate, DateTime checkOutDate);
    
    /// <summary>
    /// Ev sahibi ID'sine göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByHostIdAsync(Guid hostId);
    
    /// <summary>
    /// Bekleyen rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetPendingReservationsAsync();

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
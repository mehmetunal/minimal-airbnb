using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Reservation Repository Interface
/// </summary>
public interface IReservationRepository
{
    /// <summary>
    /// Rezervasyon ekler
    /// </summary>
    Task<Reservation> AddAsync(Reservation reservation);

    /// <summary>
    /// Rezervasyon günceller
    /// </summary>
    Task<Reservation> UpdateAsync(Reservation reservation);

    /// <summary>
    /// Rezervasyon siler
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydeder
    /// </summary>
    /// <returns>Kaydedilen değişiklik sayısı</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// ID'ye göre rezervasyon getir
    /// </summary>
    Task<Reservation?> GetByIdAsync(Guid id);

    /// <summary>
    /// Misafir ID'sine göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByGuestIdAsync(Guid guestId);

    /// <summary>
    /// Ev ID'sine göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByPropertyIdAsync(Guid propertyId);

    /// <summary>
    /// Ev sahibi ID'sine göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByHostIdAsync(Guid hostId);

    /// <summary>
    /// Duruma göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus status);

    /// <summary>
    /// Tarih aralığına göre rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Onay bekleyen rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetPendingReservationsAsync();

    /// <summary>
    /// Onaylanmış rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetConfirmedReservationsAsync();

    /// <summary>
    /// İptal edilmiş rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetCancelledReservationsAsync();

    /// <summary>
    /// Tamamlanmış rezervasyonları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetCompletedReservationsAsync();
} 
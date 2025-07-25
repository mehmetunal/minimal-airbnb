using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Reservation Repository Interface
/// </summary>
public interface IReservationRepository
{
    /// <summary>
    /// Tüm reservation'ları getir (IQueryable)
    /// </summary>
    IQueryable<Reservation> GetAll();

    /// <summary>
    /// Tüm reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetAllAsync();

    /// <summary>
    /// ID'ye göre reservation getir
    /// </summary>
    Task<Reservation?> GetByIdAsync(Guid id);

    /// <summary>
    /// ID'ye göre reservation getir (detaylı bilgilerle)
    /// </summary>
    Task<Reservation?> GetByIdWithDetailsAsync(Guid id);

    /// <summary>
    /// Property'ye göre reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByPropertyAsync(Guid propertyId);

    /// <summary>
    /// Kullanıcıya göre reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByUserAsync(Guid userId);

    /// <summary>
    /// Tarih aralığına göre reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Property ve tarih aralığına göre çakışan reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetConflictingReservationsAsync(Guid propertyId, DateTime checkInDate, DateTime checkOutDate);

    /// <summary>
    /// Duruma göre reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus status);

    /// <summary>
    /// Aktif reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetActiveReservationsAsync();

    /// <summary>
    /// Geçmiş reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetPastReservationsAsync();

    /// <summary>
    /// Gelecek reservation'ları getir
    /// </summary>
    Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync();

    /// <summary>
    /// Reservation ekle
    /// </summary>
    Task<Reservation> AddAsync(Reservation reservation);

    /// <summary>
    /// Reservation güncelle
    /// </summary>
    Task<Reservation> UpdateAsync(Reservation reservation);

    /// <summary>
    /// Reservation sil
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 
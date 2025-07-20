using MediatR;
using MinimalAirbnb.Application.DTOs.Reservation;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Queries.Reservation;

/// <summary>
/// Tüm Rezervasyonları Getirme Sorgusu
/// </summary>
public class GetAllReservationsQuery : IRequest<IEnumerable<ReservationListDto>>
{
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; } = 10;
    
    /// <summary>
    /// Misafir ID filtresi
    /// </summary>
    public Guid? GuestId { get; set; }
    
    /// <summary>
    /// Ev ID filtresi
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// Ev sahibi ID filtresi
    /// </summary>
    public Guid? HostId { get; set; }
    
    /// <summary>
    /// Durum filtresi
    /// </summary>
    public ReservationStatus? Status { get; set; }
    
    /// <summary>
    /// Başlangıç tarihi
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Bitiş tarihi
    /// </summary>
    public DateTime? EndDate { get; set; }
} 
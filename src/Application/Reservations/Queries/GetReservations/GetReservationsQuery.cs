using MediatR;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Application.Reservations.DTOs;

namespace MinimalAirbnb.Application.Reservations.Queries.GetReservations;

/// <summary>
/// Reservations listesi query'si
/// </summary>
public class GetReservationsQuery : IRequest<PagedList<ReservationDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? PropertyId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
} 
using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Reservations.DTOs;

namespace MinimalAirbnb.Application.Reservations.Queries.GetReservationById;

/// <summary>
/// Reservation detayı query'si
/// </summary>
public class GetReservationByIdQuery : IRequest<Result<ReservationDto>>
{
    public Guid Id { get; set; }
} 
using MediatR;
using MinimalAirbnb.Application.DTOs.Reservation;

namespace MinimalAirbnb.Application.Queries.Reservation;

/// <summary>
/// ID'ye Göre Rezervasyon Getirme Sorgusu
/// </summary>
public class GetReservationByIdQuery : IRequest<ReservationResultDto?>
{
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid Id { get; set; }
} 
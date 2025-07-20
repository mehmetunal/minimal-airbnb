using MediatR;
using MinimalAirbnb.Application.DTOs.Reservation;

namespace MinimalAirbnb.Application.Commands.Reservation;

/// <summary>
/// Rezervasyon Güncelleme Komutu
/// </summary>
public class UpdateReservationCommand : IRequest<ReservationResultDto>
{
    /// <summary>
    /// Rezervasyon güncelleme DTO
    /// </summary>
    public UpdateReservationDto UpdateReservationDto { get; set; } = new();
} 
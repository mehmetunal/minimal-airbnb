using MediatR;
using MinimalAirbnb.Application.DTOs.Reservation;

namespace MinimalAirbnb.Application.Commands.Reservation;

/// <summary>
/// Rezervasyon Oluşturma Komutu
/// </summary>
public class CreateReservationCommand : IRequest<ReservationResultDto>
{
    /// <summary>
    /// Rezervasyon ekleme DTO
    /// </summary>
    public AddReservationDto AddReservationDto { get; set; } = new();
} 
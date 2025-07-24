using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Reservations.DTOs;

namespace MinimalAirbnb.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Reservation oluşturma command'i
/// </summary>
public class CreateReservationCommand : IRequest<Result<CreateReservationResponseDto>>
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int GuestCount { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Notes { get; set; }
} 
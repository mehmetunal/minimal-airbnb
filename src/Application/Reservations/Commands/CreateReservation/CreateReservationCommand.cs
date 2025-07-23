using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Reservation oluşturma command'i
/// </summary>
public class CreateReservationCommand : IRequest<Result<object>>
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int GuestCount { get; set; }
} 
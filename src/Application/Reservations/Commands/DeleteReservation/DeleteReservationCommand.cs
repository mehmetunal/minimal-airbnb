using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Reservations.Commands.DeleteReservation;

/// <summary>
/// Rezervasyon silme command'i
/// </summary>
public class DeleteReservationCommand : IRequest<Result<object>>
{
    /// <summary>
    /// Silinecek rezervasyonun ID'si
    /// </summary>
    public Guid ReservationId { get; set; }
} 
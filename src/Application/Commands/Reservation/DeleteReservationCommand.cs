using MediatR;

namespace MinimalAirbnb.Application.Commands.Reservation;

/// <summary>
/// Rezervasyon Silme Komutu
/// </summary>
public class DeleteReservationCommand : IRequest<bool>
{
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid Id { get; set; }
} 
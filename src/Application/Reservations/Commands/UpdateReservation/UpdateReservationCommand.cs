using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Reservations.Commands.UpdateReservation;

/// <summary>
/// Rezervasyon güncelleme komutu
/// </summary>
public class UpdateReservationCommand : IRequest<Result<object>>
{
    public Guid ReservationId { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public int? GuestCount { get; set; }
    public decimal? PricePerNight { get; set; }
    public decimal? CleaningFee { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? TotalPrice { get; set; }
    public ReservationStatus? Status { get; set; }
    public string? SpecialRequests { get; set; }
    public string? CancellationReason { get; set; }
    public Guid? CancelledByUserId { get; set; }
    public Guid? ConfirmedByUserId { get; set; }
    public TimeSpan? CheckInTime { get; set; }
    public TimeSpan? CheckOutTime { get; set; }
} 
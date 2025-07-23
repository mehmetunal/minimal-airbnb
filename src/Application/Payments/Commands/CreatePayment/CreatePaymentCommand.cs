using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Payments.Commands.CreatePayment;

/// <summary>
/// Payment oluşturma command'i
/// </summary>
public class CreatePaymentCommand : IRequest<Result<object>>
{
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
} 
using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Payments.Commands.UpdatePayment;

/// <summary>
/// Ödeme güncelleme komutu
/// </summary>
public class UpdatePaymentCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
    public decimal? Amount { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public PaymentStatus? Status { get; set; }
    public string? TransactionId { get; set; }
    public string? Notes { get; set; }
} 
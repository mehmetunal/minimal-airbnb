using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Payments.Commands.DeletePayment;

/// <summary>
/// Ödeme silme komutu
/// </summary>
public class DeletePaymentCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
} 
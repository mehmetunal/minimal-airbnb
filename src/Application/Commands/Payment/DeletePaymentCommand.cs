using MediatR;

namespace MinimalAirbnb.Application.Commands.Payment;

/// <summary>
/// Ödeme Silme Komutu
/// </summary>
public class DeletePaymentCommand : IRequest<bool>
{
    /// <summary>
    /// Ödeme ID
    /// </summary>
    public Guid Id { get; set; }
} 
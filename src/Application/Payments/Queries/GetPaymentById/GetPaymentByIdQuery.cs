using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Payments.DTOs;

namespace MinimalAirbnb.Application.Payments.Queries.GetPaymentById;

/// <summary>
/// Payment detayı query'si
/// </summary>
public class GetPaymentByIdQuery : IRequest<Result<PaymentDto>>
{
    public Guid Id { get; set; }
} 
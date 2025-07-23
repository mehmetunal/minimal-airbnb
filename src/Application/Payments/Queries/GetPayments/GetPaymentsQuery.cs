using MediatR;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Application.Payments.DTOs;

namespace MinimalAirbnb.Application.Payments.Queries.GetPayments;

/// <summary>
/// Payments listesi query'si
/// </summary>
public class GetPaymentsQuery : IRequest<PagedList<PaymentDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? ReservationId { get; set; }
    public Guid? UserId { get; set; }
} 
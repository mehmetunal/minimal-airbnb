using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Payments.DTOs;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Payments.Queries.GetPayments;

/// <summary>
/// Payments listesi query handler'ı
/// </summary>
public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, PagedList<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentsQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PagedList<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _paymentRepository.GetAll();

            // Filters
            if (request.ReservationId.HasValue)
            {
                query = query.Where(p => p.ReservationId == request.ReservationId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == request.UserId.Value);
            }

            // Order by creation date
            query = query.OrderByDescending(p => p.CreatedDate);

            // Get paged list
            var pagedList = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            // Map to DTOs
            var paymentDtos = pagedList.Data.Select(p => new PaymentDto
            {
                Id = p.Id,
                ReservationId = p.ReservationId,
                UserId = p.UserId,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod,
                Status = p.Status,
                Provider = p.Provider,
                TransactionId = p.TransactionId,
                ProviderTransactionId = p.ProviderTransactionId,
                PaymentDate = p.PaymentDate ?? DateTime.UtcNow,
                Currency = p.Currency,
                Description = p.Description,
                RefundAmount = p.RefundAmount,
                RefundDate = p.RefundDate,
                RefundReason = p.RefundReason,
                ErrorMessage = p.ErrorMessage,
                CreatedDate = p.CreatedDate,
                ModifiedDate = p.ModifiedDate
            }).ToList();

            // Create new PagedList with DTOs
            return new PagedList<PaymentDto>(
                paymentDtos,
                request.PageNumber - 1,
                request.PageSize,
                pagedList.TotalCount
            );
        }
        catch
        {
            return new PagedList<PaymentDto>(
                new List<PaymentDto>(),
                request.PageNumber - 1,
                request.PageSize,
                0
            );
        }
    }
}

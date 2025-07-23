using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Payments.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Payments.Queries.GetPaymentById;

/// <summary>
/// Payment by ID query handler'ı
/// </summary>
public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var payment = await _paymentRepository.GetByIdAsync(request.Id);
            
            if (payment == null)
            {
                return Result<PaymentDto>.Failure(new Error("404", "Belirtilen ID'ye sahip ödeme sistemde mevcut değil."));
            }

            var paymentDto = new PaymentDto
            {
                Id = payment.Id,
                UserId = payment.UserId,
                ReservationId = payment.ReservationId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod.ToString(),
                Status = payment.Status.ToString(),
                TransactionId = payment.TransactionId,
                PaymentProvider = payment.PaymentProvider,
                ProviderReference = payment.ProviderReferenceId,
                PaymentDate = payment.PaymentDate ?? DateTime.UtcNow,
                RefundAmount = payment.RefundAmount,
                RefundDate = payment.RefundDate,
                RefundReason = payment.RefundReason,
                ErrorMessage = payment.ErrorMessage,
                Currency = payment.Currency,
                CreatedDate = payment.CreatedDate,
                UpdatedDate = payment.ModifiedDate
            };

            return Result<PaymentDto>.Success(paymentDto, new SuccessMessage("200", "Ödeme bilgileri başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<PaymentDto>.Failure(new Error("500", $"Ödeme getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 
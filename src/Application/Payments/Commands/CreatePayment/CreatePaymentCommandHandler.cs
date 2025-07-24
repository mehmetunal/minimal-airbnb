using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Payments.Commands.CreatePayment;
using MinimalAirbnb.Application.Payments.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Payments.Commands.CreatePayment;

/// <summary>
/// Payment oluşturma command handler'ı
/// </summary>
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<CreatePaymentResponseDto>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentService _paymentService;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentService paymentService)
    {
        _paymentRepository = paymentRepository;
        _paymentService = paymentService;
    }

    public async Task<Result<CreatePaymentResponseDto>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!Enum.TryParse<PaymentMethod>(request.PaymentMethod, out var paymentMethod))
            {
                return Result<CreatePaymentResponseDto>.Failure(new Error("400", "Belirtilen ödeme yöntemi sistemde tanımlı değil."));
            }

            // 1. Ödeme servisi ile ödeme işlemini başlat
            var paymentRequest = new CreatePaymentRequestDto
            {
                ReservationId = request.ReservationId,
                UserId = request.UserId,
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethod = paymentMethod,
                CardNumber = request.CardNumber,
                CardHolderName = request.CardHolderName,
                ExpiryDate = request.ExpiryDate,
                CVV = request.CVV,
                PayPalEmail = request.PayPalEmail,
                BankAccountNumber = request.BankAccountNumber,
                Description = request.Description,
                CallbackUrl = request.CallbackUrl,
                IpAddress = request.IpAddress,
                UserAgent = request.UserAgent
            };

            var paymentResult = await _paymentService.ProcessPaymentAsync(paymentRequest);

            if (!paymentResult.IsSuccess)
            {
                return Result<CreatePaymentResponseDto>.Failure(new Error("400", paymentResult.ErrorMessage));
            }

            // 2. Veritabanına ödeme kaydını ekle
            var payment = new Domain.Entities.Payment
            {
                UserId = request.UserId,
                ReservationId = request.ReservationId,
                Amount = request.Amount,
                PaymentMethod = paymentMethod,
                Status = paymentResult.Status,
                Currency = request.Currency,
                TransactionId = paymentResult.TransactionId,
                ProviderTransactionId = paymentResult.ProviderTransactionId,
                PaymentDate = paymentResult.PaymentDate,
                Description = request.Description
            };

            var createdPayment = await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            // 3. Response DTO'yu hazırla
            var responseDto = new CreatePaymentResponseDto
            {
                PaymentId = createdPayment.Id,
                ReservationId = request.ReservationId,
                UserId = request.UserId,
                Amount = request.Amount,
                PaymentMethod = paymentMethod,
                Status = paymentResult.Status,
                Provider = paymentResult.Provider,
                TransactionId = paymentResult.TransactionId,
                ProviderTransactionId = paymentResult.ProviderTransactionId,
                PaymentDate = paymentResult.PaymentDate,
                Currency = request.Currency,
                IsSuccess = paymentResult.IsSuccess,
                Message = paymentResult.Message,
                ErrorCode = paymentResult.ErrorCode,
                ErrorMessage = paymentResult.ErrorMessage
            };

            return Result<CreatePaymentResponseDto>.Success(responseDto, new SuccessMessage("200", "Ödeme başarıyla tamamlandı."));
        }
        catch (Exception ex)
        {
            return Result<CreatePaymentResponseDto>.Failure(new Error("500", $"Ödeme oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
}

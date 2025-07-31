using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Interfaces;

namespace MinimalAirbnb.Application.Payments.Commands.UpdatePayment;

/// <summary>
/// Ödeme güncelleme handler'ı
/// </summary>
public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Result<object>>
{
    private readonly IPaymentRepository _paymentRepository;

    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<object>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var payment = await _paymentRepository.GetByIdAsync(request.Id);
            if (payment == null)
            {
                return Result<object>.Failure(new Maggsoft.Core.Model.Error("PAYMENT_NOT_FOUND", "Ödeme bulunamadı."));
            }

            // Güncelleme işlemleri
            if (request.Amount.HasValue)
                payment.Amount = request.Amount.Value;
            
            if (request.PaymentMethod.HasValue)
                payment.PaymentMethod = request.PaymentMethod.Value;
            
            if (request.Status.HasValue)
                payment.Status = request.Status.Value;
            
            if (!string.IsNullOrEmpty(request.TransactionId))
                payment.TransactionId = request.TransactionId;
            
            if (!string.IsNullOrEmpty(request.Notes))
                payment.Description = request.Notes;

            await _paymentRepository.UpdateAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return Result<object>.Success("Ödeme başarıyla güncellendi.");
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Maggsoft.Core.Model.Error("PAYMENT_UPDATE_ERROR", $"Ödeme güncellenirken bir hata oluştu: {ex.Message}"));
        }
    }
} 
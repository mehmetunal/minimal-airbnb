using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Interfaces;

namespace MinimalAirbnb.Application.Payments.Commands.DeletePayment;

/// <summary>
/// Ödeme silme handler'ı
/// </summary>
public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, Result<object>>
{
    private readonly IPaymentRepository _paymentRepository;

    public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<object>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var payment = await _paymentRepository.GetByIdAsync(request.Id);
            if (payment == null)
            {
                return Result<object>.Failure(new Maggsoft.Core.Model.Error("PAYMENT_NOT_FOUND", "Ödeme bulunamadı."));
            }

            await _paymentRepository.DeleteAsync(request.Id);
            await _paymentRepository.SaveChangesAsync();

            return Result<object>.Success("Ödeme başarıyla silindi.");
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Maggsoft.Core.Model.Error("PAYMENT_DELETE_ERROR", $"Ödeme silinirken bir hata oluştu: {ex.Message}"));
        }
    }
} 
using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Payments.Commands.CreatePayment;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Payments.Commands.CreatePayment;

/// <summary>
/// Payment oluşturma command handler'ı
/// </summary>
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<object>>
{
    private readonly IPaymentRepository _paymentRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<object>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!Enum.TryParse<PaymentMethod>(request.PaymentMethod, out var paymentMethod))
            {
                return Result<object>.Failure(new Error("400", "Belirtilen ödeme yöntemi sistemde tanımlı değil."));
            }

            var payment = new MinimalAirbnb.Domain.Entities.Payment
            {
                UserId = request.UserId,
                ReservationId = request.ReservationId,
                Amount = request.Amount,
                PaymentMethod = paymentMethod,
                Status = PaymentStatus.Pending,
                Currency = "TRY"
            };

            var createdPayment = await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return Result<object>.Success(createdPayment.Id, new SuccessMessage("200", "Ödeme sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Ödeme oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
}

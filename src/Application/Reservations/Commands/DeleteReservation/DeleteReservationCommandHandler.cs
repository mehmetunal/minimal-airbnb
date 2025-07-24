using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reservations.Commands.DeleteReservation;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Reservations.Commands.DeleteReservation;

/// <summary>
/// Rezervasyon silme command handler'ı
/// </summary>
public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Result<object>>
{
    private readonly IReservationRepository _reservationRepository;

    public DeleteReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<object>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Rezervasyonu bul
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservation == null)
            {
                return Result<object>.Failure(new Error("404", "Belirtilen ID'ye sahip rezervasyon sistemde mevcut değil."));
            }

            // Rezervasyonu sil
            await _reservationRepository.DeleteAsync(request.ReservationId);
            await _reservationRepository.SaveChangesAsync();

            return Result<object>.Success(null, new SuccessMessage("200", "Rezervasyon başarıyla silindi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Rezervasyon silinirken hata oluştu: {ex.Message}"));
        }
    }
} 
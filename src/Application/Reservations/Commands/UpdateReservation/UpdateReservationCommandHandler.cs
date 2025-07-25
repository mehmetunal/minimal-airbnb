using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reservations.Commands.UpdateReservation;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Reservations.Commands.UpdateReservation;

/// <summary>
/// Rezervasyon güncelleme komut handler'ı
/// </summary>
public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Result<object>>
{
    private readonly IReservationRepository _reservationRepository;

    public UpdateReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<object>> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            
            if (reservation == null)
            {
                return Result<object>.Failure(new Error("404", "Belirtilen ID'ye sahip rezervasyon sistemde mevcut değil."));
            }

            // Sadece belirtilen alanları güncelle
            if (request.CheckInDate.HasValue)
                reservation.CheckInDate = request.CheckInDate.Value;
                
            if (request.CheckOutDate.HasValue)
                reservation.CheckOutDate = request.CheckOutDate.Value;
                
            if (request.GuestCount.HasValue)
                reservation.GuestCount = request.GuestCount.Value;
                
            if (request.PricePerNight.HasValue)
                reservation.PricePerNight = request.PricePerNight.Value;
                
            if (request.CleaningFee.HasValue)
                reservation.CleaningFee = request.CleaningFee.Value;
                
            if (request.ServiceFee.HasValue)
                reservation.ServiceFee = request.ServiceFee.Value;
                
            if (request.TotalPrice.HasValue)
                reservation.TotalPrice = request.TotalPrice.Value;
                
            if (request.Status.HasValue)
            {
                reservation.Status = request.Status.Value;
                
                // Durum değişikliklerinde tarihleri güncelle
                if (request.Status.Value == ReservationStatus.Confirmed)
                {
                    reservation.ConfirmationDate = DateTime.UtcNow;
                    reservation.ConfirmedByUserId = request.ConfirmedByUserId;
                }
                else if (request.Status.Value == ReservationStatus.Cancelled)
                {
                    reservation.CancellationDate = DateTime.UtcNow;
                    reservation.CancelledByUserId = request.CancelledByUserId;
                }
            }
                
            if (request.SpecialRequests != null)
                reservation.SpecialRequests = request.SpecialRequests;
                
            if (request.CancellationReason != null)
                reservation.CancellationReason = request.CancellationReason;
                
            if (request.CheckInTime.HasValue)
                reservation.CheckInTime = request.CheckInTime.Value;
                
            if (request.CheckOutTime.HasValue)
                reservation.CheckOutTime = request.CheckOutTime.Value;

            // Toplam günleri hesapla
            reservation.TotalDays = (int)(reservation.CheckOutDate - reservation.CheckInDate).TotalDays;

            await _reservationRepository.UpdateAsync(reservation);
            await _reservationRepository.SaveChangesAsync();

            return Result<object>.Success(new { }, new SuccessMessage("200", "Rezervasyon başarıyla güncellendi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Rezervasyon güncellenirken hata oluştu: {ex.Message}"));
        }
    }
} 
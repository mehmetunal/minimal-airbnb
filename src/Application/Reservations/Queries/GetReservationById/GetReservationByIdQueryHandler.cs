using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reservations.Queries.GetReservationById;
using MinimalAirbnb.Application.Reservations.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Reservations.Queries.GetReservationById;

/// <summary>
/// Reservation by ID query handler'ı
/// </summary>
public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Result<ReservationDto>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<ReservationDto>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var reservation = await _reservationRepository.GetByIdWithDetailsAsync(request.Id);
            
            if (reservation == null)
            {
                return Result<ReservationDto>.Failure(new Error("404", "Belirtilen ID'ye sahip rezervasyon sistemde mevcut değil."));
            }

            var reservationDto = new ReservationDto
            {
                Id = reservation.Id,
                GuestId = reservation.GuestId,
                GuestName = reservation.Guest?.FirstName + " " + reservation.Guest?.LastName,
                PropertyId = reservation.PropertyId,
                PropertyTitle = reservation.Property?.Title,
                PropertyPhotoUrl = reservation.Property?.Photos?.FirstOrDefault(p => p.IsMainPhoto)?.PhotoUrl,
                HostId = reservation.Property?.HostId ?? Guid.Empty,
                HostName = reservation.Property?.Host?.FirstName + " " + reservation.Property?.Host?.LastName,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                GuestCount = reservation.GuestCount,
                TotalDays = reservation.TotalDays,
                PricePerNight = reservation.PricePerNight,
                CleaningFee = reservation.CleaningFee,
                ServiceFee = reservation.ServiceFee,
                TotalPrice = reservation.TotalPrice,
                Status = reservation.Status,
                SpecialRequests = reservation.SpecialRequests,
                CancellationReason = reservation.CancellationReason,
                CancellationDate = reservation.CancellationDate,
                CancelledByUserId = reservation.CancelledByUserId,
                ConfirmationDate = reservation.ConfirmationDate,
                ConfirmedByUserId = reservation.ConfirmedByUserId,
                CheckInTime = reservation.CheckInTime,
                CheckOutTime = reservation.CheckOutTime
            };

            return Result<ReservationDto>.Success(reservationDto, new SuccessMessage("200", "Rezervasyon bilgileri başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<ReservationDto>.Failure(new Error("500", $"Rezervasyon getirilirken hata oluştu: {ex.Message}"));
        }
    }
}

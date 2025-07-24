using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reservations.Commands.CreateReservation;
using MinimalAirbnb.Application.Reservations.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Reservation oluşturma command handler'ı
/// </summary>
public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result<CreateReservationResponseDto>>
{
    private readonly IReservationRepository _reservationRepository;

    public CreateReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<CreateReservationResponseDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Calculate total days
            var totalDays = (request.CheckOutDate - request.CheckInDate).Days;

            // Use provided total price or calculate from property
            var totalPrice = request.TotalPrice > 0 ? request.TotalPrice : 100m * totalDays;

            var reservation = new Domain.Entities.Reservation
            {
                GuestId = request.UserId, // UserId'yi GuestId olarak kullan
                PropertyId = request.PropertyId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                GuestCount = request.GuestCount,
                TotalDays = totalDays,
                TotalPrice = totalPrice,
                SpecialRequests = request.Notes,
                Status = ReservationStatus.Pending,
                CheckInTime = new TimeSpan(15, 0, 0), // Default 15:00
                CheckOutTime = new TimeSpan(11, 0, 0)  // Default 11:00
            };

            var createdReservation = await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync();

            var responseDto = new CreateReservationResponseDto
            {
                ReservationId = createdReservation.Id,
                Message = "Rezervasyon sisteme kaydedildi.",
                IsSuccess = true
            };

            return Result<CreateReservationResponseDto>.Success(responseDto, new SuccessMessage("200", "Rezervasyon sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<CreateReservationResponseDto>.Failure(new Error("500", $"Rezervasyon oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
}

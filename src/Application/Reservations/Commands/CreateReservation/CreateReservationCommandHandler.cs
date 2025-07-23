using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reservations.Commands.CreateReservation;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Reservation oluşturma command handler'ı
/// </summary>
public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result<object>>
{
    private readonly IReservationRepository _reservationRepository;

    public CreateReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Result<object>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Calculate total days
            var totalDays = (request.CheckOutDate - request.CheckInDate).Days;
            
            // Default values for fees (these should come from property or be calculated)
            var pricePerNight = 100m; // This should come from property
            var cleaningFee = 50m;
            var serviceFee = 25m;
            var totalPrice = (pricePerNight * totalDays) + cleaningFee + serviceFee;

            var reservation = new MinimalAirbnb.Domain.Entities.Reservation
            {
                GuestId = request.UserId, // UserId'yi GuestId olarak kullan
                PropertyId = request.PropertyId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                GuestCount = request.GuestCount,
                TotalDays = totalDays,
                PricePerNight = pricePerNight,
                CleaningFee = cleaningFee,
                ServiceFee = serviceFee,
                TotalPrice = totalPrice,
                Status = ReservationStatus.Pending,
                CheckInTime = new TimeSpan(15, 0, 0), // Default 15:00
                CheckOutTime = new TimeSpan(11, 0, 0)  // Default 11:00
            };

            var createdReservation = await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync();

            return Result<object>.Success(createdReservation.Id, new SuccessMessage("200", "Rezervasyon sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Rezervasyon oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
} 
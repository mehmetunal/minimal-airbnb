using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reservations.Queries.GetReservations;
using MinimalAirbnb.Application.Reservations.DTOs;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Reservations.Queries.GetReservations;

/// <summary>
/// Reservations listesi query handler'ı
/// </summary>
public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, PagedList<ReservationDto>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationsQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<PagedList<ReservationDto>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _reservationRepository.GetAll();

            // Property filter
            if (request.PropertyId.HasValue)
            {
                query = query.Where(r => r.PropertyId == request.PropertyId.Value);
            }

            // User filter (GuestId olarak)
            if (request.UserId.HasValue)
            {
                query = query.Where(r => r.GuestId == request.UserId.Value);
            }

            // Date filters
            if (request.StartDate.HasValue)
            {
                query = query.Where(r => r.CheckInDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(r => r.CheckOutDate <= request.EndDate.Value);
            }

            // Order by creation date
            query = query.OrderByDescending(r => r.CreatedDate);

            // Get paged list
            var pagedList = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            // Map to DTOs
            var reservationDtos = pagedList.Data.Select(r => new ReservationDto
            {
                Id = r.Id,
                GuestId = r.GuestId,
                PropertyId = r.PropertyId,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                GuestCount = r.GuestCount,
                TotalDays = r.TotalDays,
                PricePerNight = r.PricePerNight,
                CleaningFee = r.CleaningFee,
                ServiceFee = r.ServiceFee,
                TotalPrice = r.TotalPrice,
                Status = r.Status,
                SpecialRequests = r.SpecialRequests,
                CancellationReason = r.CancellationReason,
                CancellationDate = r.CancellationDate,
                CancelledByUserId = r.CancelledByUserId,
                ConfirmationDate = r.ConfirmationDate,
                ConfirmedByUserId = r.ConfirmedByUserId,
                CheckInTime = r.CheckInTime,
                CheckOutTime = r.CheckOutTime
            }).ToList();

            // Create new PagedList with DTOs
            return new PagedList<ReservationDto>(
                reservationDtos,
                request.PageNumber - 1,
                request.PageSize,
                pagedList.TotalCount
            );
        }
        catch (Exception ex)
        {
            return new PagedList<ReservationDto>(
                new List<ReservationDto>(),
                request.PageNumber - 1,
                request.PageSize,
                0
            );
        }
    }
}

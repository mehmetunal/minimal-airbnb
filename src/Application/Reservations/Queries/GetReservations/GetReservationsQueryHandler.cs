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

            // User filter (GuestId olarak) - eğer UserId null ise tüm kayıtları getir
            if (request.UserId.HasValue && request.UserId.Value != Guid.Empty)
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

            // Include navigation properties
            query = query.Include(r => r.Property)
                        .ThenInclude(p => p.Host)
                        .Include(r => r.Property)
                        .ThenInclude(p => p.Photos)
                        .Include(r => r.Guest);

            // Get paged list
            var pagedList = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            // Map to DTOs
            var reservationDtos = pagedList.Data.Select(r => new ReservationDto
            {
                Id = r.Id,
                GuestId = r.GuestId,
                GuestName = r.Guest != null ? $"{r.Guest.FirstName} {r.Guest.LastName}" : string.Empty,
                PropertyId = r.PropertyId,
                PropertyTitle = r.Property?.Title ?? string.Empty,
                PropertyPhotoUrl = r.Property?.Photos?.FirstOrDefault(p => p.IsMainPhoto)?.PhotoUrl,
                HostId = r.Property?.HostId ?? Guid.Empty,
                HostName = r.Property?.Host != null ? $"{r.Property.Host.FirstName} {r.Property.Host.LastName}" : string.Empty,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                GuestCount = r.GuestCount,
                TotalDays = r.TotalDays,
                TotalNights = r.TotalDays,
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
                CheckOutTime = r.CheckOutTime,
                CreatedAt = r.CreatedDate,
                CreatedDate = r.CreatedDate
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

using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reviews.Queries.GetReviews;
using MinimalAirbnb.Application.Reviews.DTOs;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Reviews.Queries.GetReviews;

/// <summary>
/// Reviews listesi query handler'ı
/// </summary>
public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PagedList<ReviewDto>>
{
    private readonly IReviewRepository _reviewRepository;

    public GetReviewsQueryHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<PagedList<ReviewDto>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _reviewRepository.GetAll();

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

            // Rating filter
            if (request.Rating.HasValue)
            {
                query = query.Where(r => r.Rating == request.Rating.Value);
            }

            // IsApproved filter
            if (request.IsApproved.HasValue)
            {
                query = query.Where(r => r.IsApproved == request.IsApproved.Value);
            }

            // StartDate filter
            if (request.StartDate.HasValue)
            {
                query = query.Where(r => r.CreatedDate >= request.StartDate.Value);
            }

            // EndDate filter
            if (request.EndDate.HasValue)
            {
                query = query.Where(r => r.CreatedDate <= request.EndDate.Value);
            }

            // Order by creation date
            var orderedQuery = query.OrderByDescending(r => r.CreatedDate);

            // Get paged list
            var pagedList = await orderedQuery.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            // Map to DTOs
            var reviewDtos = pagedList.Data.Select(r => new ReviewDto
            {
                Id = r.Id,
                GuestId = r.GuestId,
                PropertyId = r.PropertyId,
                ReservationId = r.ReservationId,
                Rating = r.Rating,
                Comment = r.Comment,
                CleanlinessRating = r.CleanlinessRating,
                CommunicationRating = r.CommunicationRating,
                CheckInRating = r.CheckInRating,
                AccuracyRating = r.AccuracyRating,
                LocationRating = r.LocationRating,
                ValueRating = r.ValueRating,
                HostResponse = r.HostResponse,
                IsApproved = r.IsApproved,
                IsRejected = r.IsRejected,
                RejectionReason = r.RejectionReason,
                ModeratedByUserId = r.ModeratedByUserId,
                ModerationDate = r.ModerationDate,
                LikeCount = r.LikeCount,
                DislikeCount = r.DislikeCount,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.ModifiedDate,
                GuestName = r.Guest != null ? ($"{r.Guest.FirstName} {r.Guest.LastName}") : string.Empty,
                PropertyTitle = r.Property != null ? r.Property.Title : string.Empty,
                GuestPhotoUrl = r.Guest != null ? r.Guest.ProfilePicture : string.Empty
            }).ToList();

            // Create new PagedList with DTOs
            return new PagedList<ReviewDto>(
                reviewDtos,
                request.PageNumber - 1,
                request.PageSize,
                pagedList.TotalCount
            );
        }
        catch (Exception ex)
        {
            return new PagedList<ReviewDto>(
                new List<ReviewDto>(),
                request.PageNumber - 1,
                request.PageSize,
                0
            );
        }
    }
}

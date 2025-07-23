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

            // Order by creation date
            query = query.OrderByDescending(r => r.CreatedDate);

            // Get paged list
            var pagedList = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            // Map to DTOs
            var reviewDtos = pagedList.Data.Select(r => new ReviewDto
            {
                Id = r.Id,
                GuestId = r.GuestId,
                PropertyId = r.PropertyId,
                ReservationId = r.ReservationId,
                Rating = r.Rating,
                Comment = r.Comment,
                HostResponse = r.HostResponse,
                IsApproved = r.IsApproved,
                IsRejected = r.IsRejected,
                RejectionReason = r.RejectionReason,
                ModeratedByUserId = r.ModeratedByUserId,
                ModerationDate = r.ModerationDate,
                LikeCount = r.LikeCount,
                DislikeCount = r.DislikeCount
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

using MediatR;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Application.Reviews.DTOs;

namespace MinimalAirbnb.Application.Reviews.Queries.GetReviews;

/// <summary>
/// Reviews listesi query'si
/// </summary>
public class GetReviewsQuery : IRequest<PagedList<ReviewDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? PropertyId { get; set; }
    public Guid? UserId { get; set; }
} 
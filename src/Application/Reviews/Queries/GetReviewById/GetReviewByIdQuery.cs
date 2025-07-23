using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Reviews.DTOs;

namespace MinimalAirbnb.Application.Reviews.Queries.GetReviewById;

/// <summary>
/// Review detayı query'si
/// </summary>
public class GetReviewByIdQuery : IRequest<Result<ReviewDto>>
{
    public Guid Id { get; set; }
} 
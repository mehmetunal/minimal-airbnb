using MediatR;
using MinimalAirbnb.Application.DTOs.Review;

namespace MinimalAirbnb.Application.Queries.Review;

/// <summary>
/// ID'ye Göre Yorum Getirme Sorgusu
/// </summary>
public class GetReviewByIdQuery : IRequest<ReviewResultDto?>
{
    /// <summary>
    /// Yorum ID
    /// </summary>
    public Guid Id { get; set; }
} 
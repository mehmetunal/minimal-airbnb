using MediatR;
using MinimalAirbnb.Application.DTOs.Review;

namespace MinimalAirbnb.Application.Commands.Review;

/// <summary>
/// Yorum Güncelleme Komutu
/// </summary>
public class UpdateReviewCommand : IRequest<ReviewResultDto>
{
    /// <summary>
    /// Yorum güncelleme DTO
    /// </summary>
    public UpdateReviewDto UpdateReviewDto { get; set; } = new();
} 
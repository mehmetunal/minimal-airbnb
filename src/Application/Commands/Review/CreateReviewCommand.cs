using MediatR;
using MinimalAirbnb.Application.DTOs.Review;

namespace MinimalAirbnb.Application.Commands.Review;

/// <summary>
/// Yorum Oluşturma Komutu
/// </summary>
public class CreateReviewCommand : IRequest<ReviewResultDto>
{
    /// <summary>
    /// Yorum ekleme DTO
    /// </summary>
    public AddReviewDto AddReviewDto { get; set; } = new();
} 
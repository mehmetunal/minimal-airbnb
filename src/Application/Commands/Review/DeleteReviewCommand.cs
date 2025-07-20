using MediatR;

namespace MinimalAirbnb.Application.Commands.Review;

/// <summary>
/// Yorum Silme Komutu
/// </summary>
public class DeleteReviewCommand : IRequest<bool>
{
    /// <summary>
    /// Yorum ID
    /// </summary>
    public Guid Id { get; set; }
} 
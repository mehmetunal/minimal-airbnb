using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Reviews.Commands.DeleteReview;

/// <summary>
/// Yorum silme komutu
/// </summary>
public class DeleteReviewCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
} 
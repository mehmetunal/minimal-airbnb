using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Reviews.Commands.CreateReview;

/// <summary>
/// Review oluşturma command'i
/// </summary>
public class CreateReviewCommand : IRequest<Result<object>>
{
    public Guid PropertyId { get; set; }
    public Guid? ReservationId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int? CleanlinessRating { get; set; }
    public int? CommunicationRating { get; set; }
    public int? CheckInRating { get; set; }
    public int? AccuracyRating { get; set; }
    public int? LocationRating { get; set; }
    public int? ValueRating { get; set; }
} 
using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Reviews.Commands.UpdateReview;

/// <summary>
/// Yorum güncelleme komutu
/// </summary>
public class UpdateReviewCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public int? Rating { get; set; }
    public bool? IsApproved { get; set; }
    public string? AdminResponse { get; set; }
} 
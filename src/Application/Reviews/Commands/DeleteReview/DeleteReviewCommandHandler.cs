using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Interfaces;

namespace MinimalAirbnb.Application.Reviews.Commands.DeleteReview;

/// <summary>
/// Yorum silme handler'ı
/// </summary>
public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Result<object>>
{
    private readonly IReviewRepository _reviewRepository;

    public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<Result<object>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _reviewRepository.GetByIdAsync(request.Id);
            if (review == null)
            {
                return Result<object>.Failure(new Maggsoft.Core.Model.Error("REVIEW_NOT_FOUND", "Yorum bulunamadı."));
            }

            await _reviewRepository.DeleteAsync(request.Id);
            await _reviewRepository.SaveChangesAsync();

            return Result<object>.Success("Yorum başarıyla silindi.");
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Maggsoft.Core.Model.Error("REVIEW_DELETE_ERROR", $"Yorum silinirken bir hata oluştu: {ex.Message}"));
        }
    }
} 
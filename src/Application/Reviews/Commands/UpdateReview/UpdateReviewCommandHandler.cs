using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Interfaces;

namespace MinimalAirbnb.Application.Reviews.Commands.UpdateReview;

/// <summary>
/// Yorum güncelleme handler'ı
/// </summary>
public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result<object>>
{
    private readonly IReviewRepository _reviewRepository;

    public UpdateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<Result<object>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _reviewRepository.GetByIdAsync(request.Id);
            if (review == null)
            {
                return Result<object>.Failure(new Maggsoft.Core.Model.Error("REVIEW_NOT_FOUND", "Yorum bulunamadı."));
            }

            // Güncelleme işlemleri
            if (!string.IsNullOrEmpty(request.Comment))
                review.Comment = request.Comment;
            
            if (request.Rating.HasValue)
                review.Rating = request.Rating.Value;
            
            if (request.IsApproved.HasValue)
                review.IsApproved = request.IsApproved.Value;

            await _reviewRepository.UpdateAsync(review);
            await _reviewRepository.SaveChangesAsync();

            return Result<object>.Success("Yorum başarıyla güncellendi.");
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Maggsoft.Core.Model.Error("REVIEW_UPDATE_ERROR", $"Yorum güncellenirken bir hata oluştu: {ex.Message}"));
        }
    }
} 
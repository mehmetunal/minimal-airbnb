using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reviews.Commands.CreateReview;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Reviews.Commands.CreateReview;

/// <summary>
/// Review oluşturma command handler'ı
/// </summary>
public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<object>>
{
    private readonly IReviewRepository _reviewRepository;

    public CreateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<Result<object>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var review = new MinimalAirbnb.Domain.Entities.Review
            {
                GuestId = request.UserId, // UserId'yi GuestId olarak kullan
                PropertyId = request.PropertyId,
                Rating = request.Rating,
                Comment = request.Comment,
                IsApproved = false, // Default olarak onaylanmamış
                IsRejected = false,
                LikeCount = 0,
                DislikeCount = 0
            };

            var createdReview = await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();

            return Result<object>.Success(createdReview.Id, new SuccessMessage("200", "Değerlendirme sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Değerlendirme oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
} 
using MediatR;
using MinimalAirbnb.Application.Interfaces;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Reviews.Commands.CreateReview;

/// <summary>
/// Review oluşturma command handler'ı
/// </summary>
public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<CreateReviewResponseDto>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IPropertyRepository propertyRepository,
        IUserRepository userRepository)
    {
        _reviewRepository = reviewRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<CreateReviewResponseDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var review = new MinimalAirbnb.Domain.Entities.Review
            {
                GuestId = request.UserId, // UserId'yi GuestId olarak kullan
                PropertyId = request.PropertyId,
                ReservationId = request.ReservationId,
                Rating = request.Rating,
                Comment = request.Comment,
                CleanlinessRating = request.CleanlinessRating,
                CommunicationRating = request.CommunicationRating,
                CheckInRating = request.CheckInRating,
                AccuracyRating = request.AccuracyRating,
                LocationRating = request.LocationRating,
                ValueRating = request.ValueRating,
                IsApproved = false, // Default olarak onaylanmamış
                IsRejected = false,
                LikeCount = 0,
                DislikeCount = 0
            };

            var createdReview = await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();

            // Property ve User bilgilerini al
            var property = await _propertyRepository.GetByIdAsync(createdReview.PropertyId);
            var user = await _userRepository.GetByIdAsync(createdReview.GuestId);

            var responseDto = new CreateReviewResponseDto
            {
                ReviewId = createdReview.Id,
                PropertyId = createdReview.PropertyId,
                UserId = createdReview.GuestId,
                Rating = createdReview.Rating,
                Comment = createdReview.Comment,
                CreatedDate = createdReview.CreatedDate,
                PropertyTitle = property?.Title ?? "Bilinmeyen Ev",
                UserName = user?.FirstName + " " + user?.LastName ?? "Bilinmeyen Kullanıcı"
            };

            return Result<CreateReviewResponseDto>.Success(responseDto, new SuccessMessage("200", "Değerlendirme sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<CreateReviewResponseDto>.Failure(new Error("500", $"Değerlendirme oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
}

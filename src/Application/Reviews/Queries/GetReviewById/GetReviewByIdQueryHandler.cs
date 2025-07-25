using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Reviews.Queries.GetReviewById;
using MinimalAirbnb.Application.Reviews.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Reviews.Queries.GetReviewById;

/// <summary>
/// Review by ID query handler'ı
/// </summary>
public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Result<ReviewDto>>
{
    private readonly IReviewRepository _reviewRepository;

    public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<Result<ReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _reviewRepository.GetAll()
                .Include(r => r.Guest)
                .Include(r => r.Property)
                .FirstOrDefaultAsync(r => r.Id == request.Id);
            
            if (review == null)
            {
                return Result<ReviewDto>.Failure(new Error("404", "Belirtilen ID'ye sahip değerlendirme sistemde mevcut değil."));
            }

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                GuestId = review.GuestId,
                PropertyId = review.PropertyId,
                ReservationId = review.ReservationId,
                Rating = review.Rating,
                Comment = review.Comment,
                CleanlinessRating = review.CleanlinessRating,
                CommunicationRating = review.CommunicationRating,
                CheckInRating = review.CheckInRating,
                AccuracyRating = review.AccuracyRating,
                LocationRating = review.LocationRating,
                ValueRating = review.ValueRating,
                HostResponse = review.HostResponse,
                IsApproved = review.IsApproved,
                IsRejected = review.IsRejected,
                RejectionReason = review.RejectionReason,
                ModeratedByUserId = review.ModeratedByUserId,
                ModerationDate = review.ModerationDate,
                LikeCount = review.LikeCount,
                DislikeCount = review.DislikeCount,
                CreatedDate = review.CreatedDate,
                UpdatedDate = review.ModifiedDate,
                GuestName = $"{review.Guest.FirstName} {review.Guest.LastName}",
                PropertyTitle = review.Property.Title,
                GuestPhotoUrl = review.Guest.ProfilePicture
            };

            return Result<ReviewDto>.Success(reviewDto, new SuccessMessage("200", "Değerlendirme bilgileri başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<ReviewDto>.Failure(new Error("500", $"Değerlendirme getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 
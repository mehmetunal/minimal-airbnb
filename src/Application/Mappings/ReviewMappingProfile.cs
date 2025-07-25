using AutoMapper;
using MinimalAirbnb.Application.DTOs.Review;
using MinimalAirbnb.Application.Reviews.DTOs;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Yorum Mapping Profili
/// </summary>
public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        // Entity -> DTO mappings
        CreateMap<Review, ReviewResultDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.GuestPhotoUrl, opt => opt.MapFrom(src => src.Guest.ProfilePicture));

        CreateMap<Review, ReviewListDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title));

        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.GuestPhotoUrl, opt => opt.MapFrom(src => src.Guest.ProfilePicture));

        // DTO -> Entity mappings
        CreateMap<AddReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Guest, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForMember(dest => dest.Reservation, opt => opt.Ignore());

        CreateMap<UpdateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.GuestId, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.ReservationId, opt => opt.Ignore())
            .ForMember(dest => dest.Guest, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForMember(dest => dest.Reservation, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    /// <summary>
    /// Entity'yi DTO'ya dönüştür
    /// </summary>
    public ReviewDto MapToDto(Review review)
    {
        return new ReviewDto
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
            LikeCount = review.LikeCount,
            DislikeCount = review.DislikeCount,
            CreatedDate = review.CreatedDate,
            UpdatedDate = review.ModifiedDate,
            GuestName = $"{review.Guest.FirstName} {review.Guest.LastName}",
            PropertyTitle = review.Property.Title,
            GuestPhotoUrl = review.Guest.ProfilePicture
        };
    }
} 
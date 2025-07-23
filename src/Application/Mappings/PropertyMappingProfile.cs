using AutoMapper;
using MinimalAirbnb.Application.DTOs.Property;
using MinimalAirbnb.Application.Properties.DTOs;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Ev Mapping Profili
/// </summary>
public class PropertyMappingProfile : Profile
{
    public PropertyMappingProfile()
    {
        // Entity -> DTO mappings
        CreateMap<Property, PropertyResultDto>()
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => $"{src.Host.FirstName} {src.Host.LastName}"))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.TotalReviews))
            .ForMember(dest => dest.FavoriteCount, opt => opt.MapFrom(src => src.Favorites.Count))
            .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => 
                src.Photos.FirstOrDefault(p => p.IsMainPhoto) != null ? src.Photos.FirstOrDefault(p => p.IsMainPhoto).PhotoUrl : 
                (src.Photos.FirstOrDefault() != null ? src.Photos.FirstOrDefault().PhotoUrl : null)))
            .ForMember(dest => dest.Bedrooms, opt => opt.MapFrom(src => src.BedroomCount))
            .ForMember(dest => dest.Beds, opt => opt.MapFrom(src => src.BedCount))
            .ForMember(dest => dest.Bathrooms, opt => opt.MapFrom(src => src.BathroomCount))
            .ForMember(dest => dest.MaxGuests, opt => opt.MapFrom(src => src.MaxGuestCount))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => (decimal)src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => (decimal)src.Longitude));

        CreateMap<Property, PropertyListDto>()
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => $"{src.Host.FirstName} {src.Host.LastName}"))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.TotalReviews))
            .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => 
                src.Photos.FirstOrDefault(p => p.IsMainPhoto) != null ? src.Photos.FirstOrDefault(p => p.IsMainPhoto).PhotoUrl : 
                (src.Photos.FirstOrDefault() != null ? src.Photos.FirstOrDefault().PhotoUrl : null)))
            .ForMember(dest => dest.Bedrooms, opt => opt.MapFrom(src => src.BedroomCount))
            .ForMember(dest => dest.Bathrooms, opt => opt.MapFrom(src => src.BathroomCount))
            .ForMember(dest => dest.MaxGuests, opt => opt.MapFrom(src => src.MaxGuestCount));

        CreateMap<Property, PropertyDto>()
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => $"{src.Host.FirstName} {src.Host.LastName}"))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.TotalReviews))
            .ForMember(dest => dest.FavoriteCount, opt => opt.MapFrom(src => src.Favorites.Count))
            .ForMember(dest => dest.ReservationCount, opt => opt.MapFrom(src => src.Reservations.Count))
            .ForMember(dest => dest.PhotoCount, opt => opt.MapFrom(src => src.Photos.Count))
            .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => 
                src.Photos.FirstOrDefault(p => p.IsMainPhoto) != null ? src.Photos.FirstOrDefault(p => p.IsMainPhoto).PhotoUrl : 
                (src.Photos.FirstOrDefault() != null ? src.Photos.FirstOrDefault().PhotoUrl : null)))
            .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => $"{src.Address}, {src.City}, {src.Country}"))
            .ForMember(dest => dest.TotalPricePerNight, opt => opt.MapFrom(src => src.PricePerNight + src.CleaningFee + src.ServiceFee))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsPublish && !src.IsDeleted));

        // DTO -> Entity mappings
        CreateMap<AddPropertyDto, Property>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Host, opt => opt.Ignore())
            .ForMember(dest => dest.Reservations, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Photos, opt => opt.Ignore())
            .ForMember(dest => dest.Favorites, opt => opt.Ignore())
            ;

        CreateMap<UpdatePropertyDto, Property>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.HostId, opt => opt.Ignore())
            .ForMember(dest => dest.Host, opt => opt.Ignore())
            .ForMember(dest => dest.Reservations, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Photos, opt => opt.Ignore())
            .ForMember(dest => dest.Favorites, opt => opt.Ignore())

            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
} 
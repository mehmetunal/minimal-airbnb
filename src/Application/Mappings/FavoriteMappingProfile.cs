using AutoMapper;
using MinimalAirbnb.Application.DTOs.Favorite;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Favori Mapping Profili
/// </summary>
public class FavoriteMappingProfile : Profile
{
    public FavoriteMappingProfile()
    {
        // Entity -> DTO mappings
        CreateMap<Favorite, FavoriteResultDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => $"{src.Property.Host.FirstName} {src.Property.Host.LastName}"))
            .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => 
                src.Property.Photos.FirstOrDefault(p => p.IsMainPhoto) != null ? src.Property.Photos.FirstOrDefault(p => p.IsMainPhoto).PhotoUrl : 
                (src.Property.Photos.FirstOrDefault() != null ? src.Property.Photos.FirstOrDefault().PhotoUrl : null)));

        CreateMap<Favorite, FavoriteListDto>()
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => $"{src.Property.Host.FirstName} {src.Property.Host.LastName}"))
            .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.Property.PricePerNight))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Property.City))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Property.AverageRating))
            .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => 
                src.Property.Photos.FirstOrDefault(p => p.IsMainPhoto) != null ? src.Property.Photos.FirstOrDefault(p => p.IsMainPhoto).PhotoUrl : 
                (src.Property.Photos.FirstOrDefault() != null ? src.Property.Photos.FirstOrDefault().PhotoUrl : null)));

        // DTO -> Entity mappings
        CreateMap<AddFavoriteDto, Favorite>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore());

        CreateMap<UpdateFavoriteDto, Favorite>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
} 
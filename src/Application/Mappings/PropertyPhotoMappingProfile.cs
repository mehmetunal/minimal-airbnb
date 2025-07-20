using AutoMapper;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Ev Fotoğrafı Mapping Profili
/// </summary>
public class PropertyPhotoMappingProfile : Profile
{
    public PropertyPhotoMappingProfile()
    {
        // Entity -> DTO mappings
        CreateMap<PropertyPhoto, PropertyPhotoResultDto>()
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Caption))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Caption))
            .ForMember(dest => dest.IsMain, opt => opt.MapFrom(src => src.IsMainPhoto))
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.SortOrder));

        CreateMap<PropertyPhoto, PropertyPhotoListDto>()
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Caption))
            .ForMember(dest => dest.IsMain, opt => opt.MapFrom(src => src.IsMainPhoto))
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.SortOrder));

        // DTO -> Entity mappings
        CreateMap<AddPropertyPhotoDto, PropertyPhoto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.IsMainPhoto, opt => opt.MapFrom(src => src.IsMain))
            .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.Order))
            .ForMember(dest => dest.FileSize, opt => opt.Ignore())
            .ForMember(dest => dest.FileType, opt => opt.Ignore());

        CreateMap<UpdatePropertyPhotoDto, PropertyPhoto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.IsMainPhoto, opt => opt.MapFrom(src => src.IsMain))
            .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.Order))
            .ForMember(dest => dest.FileSize, opt => opt.Ignore())
            .ForMember(dest => dest.FileType, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
} 
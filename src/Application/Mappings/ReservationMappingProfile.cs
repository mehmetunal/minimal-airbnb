using AutoMapper;
using MinimalAirbnb.Application.DTOs.Reservation;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Rezervasyon Mapping Profili
/// </summary>
public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        // Entity -> DTO mappings
        CreateMap<Reservation, ReservationResultDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => $"{src.Property.Host.FirstName} {src.Property.Host.LastName}"))
            .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => src.Property.HostId));

        CreateMap<Reservation, ReservationListDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => $"{src.Property.Host.FirstName} {src.Property.Host.LastName}"));

        // DTO -> Entity mappings
        CreateMap<AddReservationDto, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Guest, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Messages, opt => opt.Ignore())
            .ForMember(dest => dest.CancelledByUser, opt => opt.Ignore())
            .ForMember(dest => dest.ConfirmedByUser, opt => opt.Ignore());

        CreateMap<UpdateReservationDto, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.GuestId, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.Guest, opt => opt.Ignore())
            .ForMember(dest => dest.Property, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Payments, opt => opt.Ignore())
            .ForMember(dest => dest.Messages, opt => opt.Ignore())
            .ForMember(dest => dest.CancelledByUser, opt => opt.Ignore())
            .ForMember(dest => dest.ConfirmedByUser, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
using AutoMapper;
using MinimalAirbnb.Application.Reservations.DTOs;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Reservation Mapping Profile
/// </summary>
public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        // Reservation -> ReservationDto
        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => 
                src.Guest != null ? $"{src.Guest.FirstName} {src.Guest.LastName}" : string.Empty))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.Title : string.Empty))
            .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.HostId : Guid.Empty))
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => 
                src.Property != null && src.Property.Host != null ? $"{src.Property.Host.FirstName} {src.Property.Host.LastName}" : string.Empty))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.ModifiedDate));

        // Reservation -> ReservationListDto
        CreateMap<Reservation, DTOs.Reservation.ReservationListDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => 
                src.Guest != null ? $"{src.Guest.FirstName} {src.Guest.LastName}" : string.Empty))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.Title : string.Empty))
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => 
                src.Property != null && src.Property.Host != null ? $"{src.Property.Host.FirstName} {src.Property.Host.LastName}" : string.Empty))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate));

        // Reservation -> ReservationResultDto
        CreateMap<Reservation, DTOs.Reservation.ReservationResultDto>()
            .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => 
                src.Guest != null ? $"{src.Guest.FirstName} {src.Guest.LastName}" : string.Empty))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.Title : string.Empty))
            .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.HostId : Guid.Empty))
            .ForMember(dest => dest.HostName, opt => opt.MapFrom(src => 
                src.Property != null && src.Property.Host != null ? $"{src.Property.Host.FirstName} {src.Property.Host.LastName}" : string.Empty))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.ModifiedDate));

        // AddReservationDto -> Reservation
        CreateMap<DTOs.Reservation.AddReservationDto, Reservation>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // UpdateReservationDto -> Reservation
        CreateMap<DTOs.Reservation.UpdateReservationDto, Reservation>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
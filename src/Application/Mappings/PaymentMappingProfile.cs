using AutoMapper;
using MinimalAirbnb.Application.DTOs.Payment;
using MinimalAirbnb.Application.Payments.DTOs;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Ödeme Mapping Profili
/// </summary>
public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        // Entity -> DTO mappings
        CreateMap<Payment, PaymentResultDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Reservation.Property.Title));

        CreateMap<Payment, PaymentListDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Reservation.Property.Title));

        CreateMap<Payment, PaymentDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Reservation.Property.Title));

        // DTO -> Entity mappings
        CreateMap<AddPaymentDto, Payment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Reservation, opt => opt.Ignore());

        CreateMap<UpdatePaymentDto, Payment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.ReservationId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Reservation, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    /// <summary>
    /// Entity'yi DTO'ya dönüştür
    /// </summary>
    public PaymentDto MapToDto(Payment payment)
    {
        return new PaymentDto
        {
            Id = payment.Id,
            UserId = payment.UserId,
            ReservationId = payment.ReservationId,
            Amount = payment.Amount,
            PaymentMethod = payment.PaymentMethod.ToString(),
            Status = payment.Status.ToString(),
            PaymentDate = payment.PaymentDate ?? DateTime.UtcNow,
            TransactionId = payment.TransactionId,
            UserName = $"{payment.User.FirstName} {payment.User.LastName}",
            PropertyTitle = payment.Reservation.Property.Title
        };
    }
} 
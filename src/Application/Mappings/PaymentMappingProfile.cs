using AutoMapper;
using MinimalAirbnb.Application.Payments.DTOs;
using MinimalAirbnb.Application.Payments.Commands.CreatePayment;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Payment mapping profile'ı
/// </summary>
public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        // Payment -> PaymentDto
        CreateMap<Payment, PaymentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
            .ForMember(dest => dest.ProviderTransactionId, opt => opt.MapFrom(src => src.ProviderTransactionId))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.RefundAmount, opt => opt.MapFrom(src => src.RefundAmount))
            .ForMember(dest => dest.RefundDate, opt => opt.MapFrom(src => src.RefundDate))
            .ForMember(dest => dest.RefundReason, opt => opt.MapFrom(src => src.RefundReason))
            .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate));

        // CreatePaymentCommand -> Payment
        CreateMap<CreatePaymentCommand, Payment>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Enums.PaymentStatus.Pending))
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => Domain.Enums.PaymentProvider.TestProvider));

        // Payment -> CreatePaymentResponseDto
        CreateMap<Payment, CreatePaymentResponseDto>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
            .ForMember(dest => dest.ProviderTransactionId, opt => opt.MapFrom(src => src.ProviderTransactionId))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Status == Domain.Enums.PaymentStatus.Success))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => 
                src.Status == Domain.Enums.PaymentStatus.Success ? "Ödeme başarıyla tamamlandı." : 
                src.Status == Domain.Enums.PaymentStatus.Failed ? "Ödeme başarısız." : 
                src.Status == Domain.Enums.PaymentStatus.Pending ? "Ödeme bekleniyor." : "Ödeme işleniyor."));
    }
} 
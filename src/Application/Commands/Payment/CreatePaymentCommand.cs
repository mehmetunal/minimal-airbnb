using MediatR;
using MinimalAirbnb.Application.DTOs.Payment;
using MinimalAirbnb.Domain.Enums;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Commands.Payment;

/// <summary>
/// Ödeme Oluşturma Komutu
/// </summary>
public class CreatePaymentCommand : IRequest<Result<PaymentResultDto>>
{
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid ReservationId { get; set; }
    
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Misafir ID
    /// </summary>
    public Guid GuestId { get; set; }
    
    /// <summary>
    /// Ödeme tutarı
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Para birimi
    /// </summary>
    public string Currency { get; set; } = "TRY";
    
    /// <summary>
    /// Ödeme yöntemi
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }
    
    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// İşlem ID'si
    /// </summary>
    public string? TransactionId { get; set; }
    
    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime PaymentDate { get; set; }
    
    /// <summary>
    /// Ödeme açıklaması
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Kart numarası
    /// </summary>
    public string? CardNumber { get; set; }

    /// <summary>
    /// Kart sahibi adı
    /// </summary>
    public string? CardHolderName { get; set; }

    /// <summary>
    /// Son kullanma tarihi
    /// </summary>
    public string? ExpiryDate { get; set; }

    /// <summary>
    /// CVV
    /// </summary>
    public string? CVV { get; set; }

    /// <summary>
    /// PayPal e-posta
    /// </summary>
    public string? PayPalEmail { get; set; }

    /// <summary>
    /// Banka hesap numarası
    /// </summary>
    public string? BankAccountNumber { get; set; }

    /// <summary>
    /// Callback URL
    /// </summary>
    public string? CallbackUrl { get; set; }

    /// <summary>
    /// IP adresi
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// User Agent
    /// </summary>
    public string? UserAgent { get; set; }
} 
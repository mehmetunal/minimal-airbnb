using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Payments.DTOs;

/// <summary>
/// Ödeme oluşturma isteği DTO'su
/// </summary>
public class CreatePaymentRequestDto
{
    /// <summary>
    /// Rezervasyon ID'si
    /// </summary>
    public Guid ReservationId { get; set; }
    
    /// <summary>
    /// Kullanıcı ID'si
    /// </summary>
    public Guid UserId { get; set; }
    
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
    /// Ödeme sağlayıcısı (opsiyonel, otomatik seçilir)
    /// </summary>
    public PaymentProvider? Provider { get; set; }
    
    /// <summary>
    /// Kart numarası (kredi/banka kartı için)
    /// </summary>
    public string? CardNumber { get; set; }
    
    /// <summary>
    /// Kart sahibi adı
    /// </summary>
    public string? CardHolderName { get; set; }
    
    /// <summary>
    /// Son kullanma tarihi (MM/YY formatında)
    /// </summary>
    public string? ExpiryDate { get; set; }
    
    /// <summary>
    /// CVV
    /// </summary>
    public string? CVV { get; set; }
    
    /// <summary>
    /// PayPal email (PayPal için)
    /// </summary>
    public string? PayPalEmail { get; set; }
    
    /// <summary>
    /// Banka hesap numarası (banka transferi için)
    /// </summary>
    public string? BankAccountNumber { get; set; }
    
    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Callback URL (ödeme sonrası yönlendirilecek URL)
    /// </summary>
    public string? CallbackUrl { get; set; }
    
    /// <summary>
    /// IP adresi
    /// </summary>
    public string? IpAddress { get; set; }
    
    /// <summary>
    /// Kullanıcı agent
    /// </summary>
    public string? UserAgent { get; set; }
} 
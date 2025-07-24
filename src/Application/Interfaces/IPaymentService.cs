using MinimalAirbnb.Application.Payments.DTOs;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Ödeme servisi interface'i
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Ödeme işlemi başlat
    /// </summary>
    /// <param name="request">Ödeme isteği</param>
    /// <returns>Ödeme sonucu</returns>
    Task<CreatePaymentResponseDto> ProcessPaymentAsync(CreatePaymentRequestDto request);
    
    /// <summary>
    /// Ödeme durumunu kontrol et
    /// </summary>
    /// <param name="transactionId">İşlem ID'si</param>
    /// <returns>Ödeme durumu</returns>
    Task<PaymentStatus> CheckPaymentStatusAsync(string transactionId);
    
    /// <summary>
    /// Ödeme iadesi yap
    /// </summary>
    /// <param name="paymentId">Ödeme ID'si</param>
    /// <param name="amount">İade tutarı</param>
    /// <param name="reason">İade sebebi</param>
    /// <returns>İade sonucu</returns>
    Task<CreatePaymentResponseDto> RefundPaymentAsync(Guid paymentId, decimal amount, string reason);
    
    /// <summary>
    /// Ödeme iptal et
    /// </summary>
    /// <param name="paymentId">Ödeme ID'si</param>
    /// <param name="reason">İptal sebebi</param>
    /// <returns>İptal sonucu</returns>
    Task<CreatePaymentResponseDto> CancelPaymentAsync(Guid paymentId, string reason);
    
    /// <summary>
    /// Ödeme sağlayıcısını seç
    /// </summary>
    /// <param name="paymentMethod">Ödeme yöntemi</param>
    /// <param name="amount">Tutar</param>
    /// <returns>Ödeme sağlayıcısı</returns>
    PaymentProvider SelectPaymentProvider(PaymentMethod paymentMethod, decimal amount);
    
    /// <summary>
    /// Kart bilgilerini doğrula
    /// </summary>
    /// <param name="cardNumber">Kart numarası</param>
    /// <param name="expiryDate">Son kullanma tarihi</param>
    /// <param name="cvv">CVV</param>
    /// <returns>Doğrulama sonucu</returns>
    Task<bool> ValidateCardAsync(string cardNumber, string expiryDate, string cvv);
} 
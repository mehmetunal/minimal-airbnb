using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Payments.DTOs;
using MinimalAirbnb.Domain.Enums;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MinimalAirbnb.Infrastructure.Services;

/// <summary>
/// Ödeme servisi implementasyonu
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly ILogger<PaymentService> _logger;
    private readonly IConfiguration _configuration;

    public PaymentService(ILogger<PaymentService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Ödeme işlemi başlat
    /// </summary>
    public async Task<CreatePaymentResponseDto> ProcessPaymentAsync(CreatePaymentRequestDto request)
    {
        try
        {
            _logger.LogInformation("Ödeme işlemi başlatılıyor. ReservationId: {ReservationId}, Amount: {Amount}", 
                request.ReservationId, request.Amount);

            // 1. Kart bilgilerini doğrula
            if (request.PaymentMethod == PaymentMethod.CreditCard || request.PaymentMethod == PaymentMethod.DebitCard)
            {
                if (!await ValidateCardAsync(request.CardNumber!, request.ExpiryDate!, request.CVV!))
                {
                    return new CreatePaymentResponseDto
                    {
                        PaymentId = Guid.Empty,
                        ReservationId = request.ReservationId,
                        UserId = request.UserId,
                        Amount = request.Amount,
                        PaymentMethod = request.PaymentMethod,
                        Status = PaymentStatus.Failed,
                        Provider = PaymentProvider.TestProvider,
                        TransactionId = Guid.NewGuid().ToString(),
                        PaymentDate = DateTime.UtcNow,
                        IsSuccess = false,
                        ErrorCode = "INVALID_CARD",
                        ErrorMessage = "Kart bilgileri geçersiz."
                    };
                }
            }

            // 2. Ödeme sağlayıcısını seç
            var provider = request.Provider ?? SelectPaymentProvider(request.PaymentMethod, request.Amount);

            // 3. Ödeme sağlayıcısına göre işlem yap
            var result = await ProcessPaymentWithProviderAsync(request, provider);

            _logger.LogInformation("Ödeme işlemi tamamlandı. TransactionId: {TransactionId}, Status: {Status}", 
                result.TransactionId, result.Status);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ödeme işlemi sırasında hata oluştu. ReservationId: {ReservationId}", request.ReservationId);
            
            return new CreatePaymentResponseDto
            {
                PaymentId = Guid.Empty,
                ReservationId = request.ReservationId,
                UserId = request.UserId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                Status = PaymentStatus.Failed,
                Provider = PaymentProvider.TestProvider,
                TransactionId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.UtcNow,
                IsSuccess = false,
                ErrorCode = "PROCESSING_ERROR",
                ErrorMessage = "Ödeme işlemi sırasında bir hata oluştu."
            };
        }
    }

    /// <summary>
    /// Ödeme durumunu kontrol et
    /// </summary>
    public async Task<PaymentStatus> CheckPaymentStatusAsync(string transactionId)
    {
        try
        {
            _logger.LogInformation("Ödeme durumu kontrol ediliyor. TransactionId: {TransactionId}", transactionId);

            // TODO: Gerçek ödeme sağlayıcısı entegrasyonu burada yapılacak
            // Şimdilik test amaçlı başarılı döndürüyoruz
            await Task.Delay(100); // Simüle edilmiş API çağrısı

            return PaymentStatus.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ödeme durumu kontrol edilirken hata oluştu. TransactionId: {TransactionId}", transactionId);
            return PaymentStatus.Failed;
        }
    }

    /// <summary>
    /// Ödeme iadesi yap
    /// </summary>
    public async Task<CreatePaymentResponseDto> RefundPaymentAsync(Guid paymentId, decimal amount, string reason)
    {
        try
        {
            _logger.LogInformation("Ödeme iadesi başlatılıyor. PaymentId: {PaymentId}, Amount: {Amount}", paymentId, amount);

            // TODO: Gerçek ödeme sağlayıcısı entegrasyonu burada yapılacak
            await Task.Delay(100); // Simüle edilmiş API çağrısı

            return new CreatePaymentResponseDto
            {
                PaymentId = paymentId,
                ReservationId = Guid.Empty,
                UserId = Guid.Empty,
                Amount = amount,
                PaymentMethod = PaymentMethod.CreditCard,
                Status = PaymentStatus.Refunded,
                Provider = PaymentProvider.TestProvider,
                TransactionId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.UtcNow,
                IsSuccess = true,
                Message = "İade işlemi başarıyla tamamlandı."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ödeme iadesi sırasında hata oluştu. PaymentId: {PaymentId}", paymentId);
            
            return new CreatePaymentResponseDto
            {
                PaymentId = paymentId,
                ReservationId = Guid.Empty,
                UserId = Guid.Empty,
                Amount = amount,
                PaymentMethod = PaymentMethod.CreditCard,
                Status = PaymentStatus.Failed,
                Provider = PaymentProvider.TestProvider,
                TransactionId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.UtcNow,
                IsSuccess = false,
                ErrorCode = "REFUND_ERROR",
                ErrorMessage = "İade işlemi sırasında bir hata oluştu."
            };
        }
    }

    /// <summary>
    /// Ödeme iptal et
    /// </summary>
    public async Task<CreatePaymentResponseDto> CancelPaymentAsync(Guid paymentId, string reason)
    {
        try
        {
            _logger.LogInformation("Ödeme iptali başlatılıyor. PaymentId: {PaymentId}", paymentId);

            // TODO: Gerçek ödeme sağlayıcısı entegrasyonu burada yapılacak
            await Task.Delay(100); // Simüle edilmiş API çağrısı

            return new CreatePaymentResponseDto
            {
                PaymentId = paymentId,
                ReservationId = Guid.Empty,
                UserId = Guid.Empty,
                Amount = 0,
                PaymentMethod = PaymentMethod.CreditCard,
                Status = PaymentStatus.Cancelled,
                Provider = PaymentProvider.TestProvider,
                TransactionId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.UtcNow,
                IsSuccess = true,
                Message = "Ödeme iptali başarıyla tamamlandı."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ödeme iptali sırasında hata oluştu. PaymentId: {PaymentId}", paymentId);
            
            return new CreatePaymentResponseDto
            {
                PaymentId = paymentId,
                ReservationId = Guid.Empty,
                UserId = Guid.Empty,
                Amount = 0,
                PaymentMethod = PaymentMethod.CreditCard,
                Status = PaymentStatus.Failed,
                Provider = PaymentProvider.TestProvider,
                TransactionId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.UtcNow,
                IsSuccess = false,
                ErrorCode = "CANCEL_ERROR",
                ErrorMessage = "Ödeme iptali sırasında bir hata oluştu."
            };
        }
    }

    /// <summary>
    /// Ödeme sağlayıcısını seç
    /// </summary>
    public PaymentProvider SelectPaymentProvider(PaymentMethod paymentMethod, decimal amount)
    {
        // Basit bir sağlayıcı seçim mantığı
        return paymentMethod switch
        {
            PaymentMethod.CreditCard or PaymentMethod.DebitCard => PaymentProvider.Iyzico,
            PaymentMethod.PayPal => PaymentProvider.PayPal,
            PaymentMethod.ApplePay => PaymentProvider.Stripe,
            PaymentMethod.GooglePay => PaymentProvider.Stripe,
            PaymentMethod.BankTransfer => PaymentProvider.Iyzico,
            PaymentMethod.Cash => PaymentProvider.TestProvider,
            _ => PaymentProvider.Iyzico
        };
    }

    /// <summary>
    /// Kart bilgilerini doğrula
    /// </summary>
    public async Task<bool> ValidateCardAsync(string cardNumber, string expiryDate, string cvv)
    {
        try
        {
            // Kart numarası kontrolü (Luhn algoritması)
            if (!IsValidCardNumber(cardNumber))
                return false;

            // Son kullanma tarihi kontrolü
            if (!IsValidExpiryDate(expiryDate))
                return false;

            // CVV kontrolü
            if (!IsValidCVV(cvv))
                return false;

            // Test kartları kontrolü (geliştirme ortamı için)
            if (IsTestCard(cardNumber))
            {
                _logger.LogInformation("Test kartı kullanıldı: {CardNumber}", MaskCardNumber(cardNumber));
                return true;
            }

            // TODO: Gerçek kart doğrulama servisi entegrasyonu burada yapılacak
            await Task.Delay(50); // Simüle edilmiş API çağrısı

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Kart doğrulama sırasında hata oluştu");
            return false;
        }
    }

    /// <summary>
    /// Ödeme sağlayıcısı ile ödeme işlemi
    /// </summary>
    private async Task<CreatePaymentResponseDto> ProcessPaymentWithProviderAsync(CreatePaymentRequestDto request, PaymentProvider provider)
    {
        // Test kartları için özel işlem
        if (request.PaymentMethod == PaymentMethod.CreditCard || request.PaymentMethod == PaymentMethod.DebitCard)
        {
            if (!string.IsNullOrEmpty(request.CardNumber) && IsTestCard(request.CardNumber))
            {
                var testStatus = SimulateTestCardResult(request.CardNumber);
                var testSuccess = testStatus == PaymentStatus.Success;
                
                _logger.LogInformation("Test kartı ile ödeme işlemi. Kart: {CardNumber}, Sonuç: {Status}", 
                    MaskCardNumber(request.CardNumber), testStatus);

                return new CreatePaymentResponseDto
                {
                    PaymentId = Guid.NewGuid(),
                    ReservationId = request.ReservationId,
                    UserId = request.UserId,
                    Amount = request.Amount,
                    PaymentMethod = request.PaymentMethod,
                    Status = testStatus,
                    Provider = provider,
                    TransactionId = $"TXN_{Guid.NewGuid():N}",
                    ProviderTransactionId = $"PROV_{Guid.NewGuid():N}",
                    PaymentDate = DateTime.UtcNow,
                    Currency = request.Currency,
                    IsSuccess = testSuccess,
                    Message = testSuccess ? "Test ödeme başarıyla tamamlandı." : "Test ödeme başarısız.",
                    ErrorCode = testSuccess ? "" : "TEST_PAYMENT_FAILED",
                    ErrorMessage = testSuccess ? "" : "Test kartı ile ödeme başarısız."
                };
            }
        }

        // TODO: Gerçek ödeme sağlayıcısı entegrasyonu burada yapılacak
        await Task.Delay(200); // Simüle edilmiş API çağrısı

        var transactionId = Guid.NewGuid().ToString();
        var isSuccess = new Random().Next(1, 10) > 2; // %80 başarı oranı

        return new CreatePaymentResponseDto
        {
            PaymentId = Guid.NewGuid(),
            ReservationId = request.ReservationId,
            UserId = request.UserId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            Status = isSuccess ? PaymentStatus.Success : PaymentStatus.Failed,
            Provider = provider,
            TransactionId = transactionId,
            ProviderTransactionId = $"PROVIDER_{transactionId}",
            PaymentDate = DateTime.UtcNow,
            Currency = request.Currency,
            IsSuccess = isSuccess,
            Message = isSuccess ? "Ödeme başarıyla tamamlandı." : "Ödeme işlemi reddedildi.",
            ErrorCode = isSuccess ? "" : "PAYMENT_FAILED",
            ErrorMessage = isSuccess ? "" : "Ödeme işlemi reddedildi."
        };
    }

    /// <summary>
    /// Kart numarası geçerliliğini kontrol et (Luhn algoritması)
    /// </summary>
    private bool IsValidCardNumber(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return false;

        var cleanNumber = cardNumber.Replace(" ", "").Replace("-", "");
        
        if (cleanNumber.Length < 13 || cleanNumber.Length > 19)
            return false;

        if (!cleanNumber.All(char.IsDigit))
            return false;

        // Luhn algoritması
        var sum = 0;
        var isEven = false;

        for (int i = cleanNumber.Length - 1; i >= 0; i--)
        {
            var digit = int.Parse(cleanNumber[i].ToString());

            if (isEven)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            isEven = !isEven;
        }

        return sum % 10 == 0;
    }

    /// <summary>
    /// Son kullanma tarihi geçerliliğini kontrol et
    /// </summary>
    private bool IsValidExpiryDate(string expiryDate)
    {
        if (string.IsNullOrWhiteSpace(expiryDate))
            return false;

        if (!expiryDate.Contains('/'))
            return false;

        var parts = expiryDate.Split('/');
        if (parts.Length != 2)
            return false;

        if (!int.TryParse(parts[0], out var month) || !int.TryParse(parts[1], out var year))
            return false;

        if (month < 1 || month > 12)
            return false;

        var currentYear = DateTime.Now.Year % 100;
        var currentMonth = DateTime.Now.Month;

        if (year < currentYear || (year == currentYear && month < currentMonth))
            return false;

        return true;
    }

    /// <summary>
    /// CVV geçerliliğini kontrol et
    /// </summary>
    private bool IsValidCVV(string cvv)
    {
        if (string.IsNullOrWhiteSpace(cvv))
            return false;

        if (!cvv.All(char.IsDigit))
            return false;

        return cvv.Length >= 3 && cvv.Length <= 4;
    }

    /// <summary>
    /// Test kartı mı kontrol et
    /// </summary>
    private bool IsTestCard(string cardNumber)
    {
        var cleanNumber = cardNumber.Replace(" ", "").Replace("-", "");
        
        // Test kart numaraları
        var testCards = new[]
        {
            "4111111111111111", // Visa test kartı
            "5555555555554444", // MasterCard test kartı
            "378282246310005",  // American Express test kartı
            "6011111111111117", // Discover test kartı
            "4000000000000002", // Visa (başarısız ödeme)
            "4000000000009995", // Visa (yetersiz bakiye)
            "4000000000009987", // Visa (kart iptal)
            "4000000000009979", // Visa (kart çalınmış)
            "4000000000000069", // Visa (yanlış CVV)
            "4000000000000127"  // Visa (yanlış son kullanma tarihi)
        };

        return testCards.Contains(cleanNumber);
    }

    /// <summary>
    /// Kart numarasını maskele
    /// </summary>
    private string MaskCardNumber(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return string.Empty;

        var cleanNumber = cardNumber.Replace(" ", "").Replace("-", "");
        
        if (cleanNumber.Length < 4)
            return cardNumber;

        return $"{cleanNumber.Substring(0, 4)} **** **** {cleanNumber.Substring(cleanNumber.Length - 4)}";
    }

    /// <summary>
    /// Test kartı sonucunu simüle et
    /// </summary>
    private PaymentStatus SimulateTestCardResult(string cardNumber)
    {
        var cleanNumber = cardNumber.Replace(" ", "").Replace("-", "");
        
        return cleanNumber switch
        {
            "4000000000000002" => PaymentStatus.Failed,
            "4000000000009995" => PaymentStatus.Failed,
            "4000000000009987" => PaymentStatus.Failed,
            "4000000000009979" => PaymentStatus.Failed,
            "4000000000000069" => PaymentStatus.Failed,
            "4000000000000127" => PaymentStatus.Failed,
            _ => PaymentStatus.Success
        };
    }
} 
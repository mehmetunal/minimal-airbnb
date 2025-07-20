namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Ödeme yöntemleri
/// </summary>
public enum PaymentMethod
{
    /// <summary>
    /// Kredi kartı
    /// </summary>
    CreditCard = 1,

    /// <summary>
    /// Banka kartı
    /// </summary>
    DebitCard = 2,

    /// <summary>
    /// PayPal
    /// </summary>
    PayPal = 3,

    /// <summary>
    /// Apple Pay
    /// </summary>
    ApplePay = 4,

    /// <summary>
    /// Google Pay
    /// </summary>
    GooglePay = 5,

    /// <summary>
    /// Banka havalesi
    /// </summary>
    BankTransfer = 6,

    /// <summary>
    /// Nakit
    /// </summary>
    Cash = 7,

    /// <summary>
    /// Çek
    /// </summary>
    Check = 8,

    /// <summary>
    /// Kripto para
    /// </summary>
    Cryptocurrency = 9,

    /// <summary>
    /// Diğer
    /// </summary>
    Other = 99
} 
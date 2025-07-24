namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Ödeme yöntemleri
/// </summary>
public enum PaymentMethod
{
    /// <summary>
    /// Kredi Kartı
    /// </summary>
    CreditCard = 0,
    
    /// <summary>
    /// Banka Kartı
    /// </summary>
    DebitCard = 1,
    
    /// <summary>
    /// PayPal
    /// </summary>
    PayPal = 2,
    
    /// <summary>
    /// Apple Pay
    /// </summary>
    ApplePay = 3,
    
    /// <summary>
    /// Google Pay
    /// </summary>
    GooglePay = 4,
    
    /// <summary>
    /// Banka Transferi
    /// </summary>
    BankTransfer = 5,
    
    /// <summary>
    /// Nakit
    /// </summary>
    Cash = 6
} 
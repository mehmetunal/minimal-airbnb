namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Ödeme sağlayıcıları
/// </summary>
public enum PaymentProvider
{
    /// <summary>
    /// Iyzico
    /// </summary>
    Iyzico = 0,
    
    /// <summary>
    /// PayTR
    /// </summary>
    PayTR = 1,
    
    /// <summary>
    /// Stripe
    /// </summary>
    Stripe = 2,
    
    /// <summary>
    /// PayPal
    /// </summary>
    PayPal = 3,
    
    /// <summary>
    /// Adyen
    /// </summary>
    Adyen = 4,
    
    /// <summary>
    /// Square
    /// </summary>
    Square = 5,
    
    /// <summary>
    /// Test Provider (Geliştirme için)
    /// </summary>
    TestProvider = 99
} 
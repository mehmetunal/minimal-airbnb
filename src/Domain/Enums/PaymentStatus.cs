namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Ödeme durumları
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Beklemede
    /// </summary>
    Pending = 0,
    
    /// <summary>
    /// Başarılı
    /// </summary>
    Success = 1,
    
    /// <summary>
    /// Başarısız
    /// </summary>
    Failed = 2,
    
    /// <summary>
    /// İptal edildi
    /// </summary>
    Cancelled = 3,
    
    /// <summary>
    /// İade edildi
    /// </summary>
    Refunded = 4,
    
    /// <summary>
    /// İşleniyor
    /// </summary>
    Processing = 5
} 
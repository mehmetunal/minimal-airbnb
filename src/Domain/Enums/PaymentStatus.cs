namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Ödeme durumları
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Beklemede - Ödeme bekleniyor
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Başarılı - Ödeme başarıyla tamamlandı
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Başarısız - Ödeme başarısız oldu
    /// </summary>
    Failed = 3,

    /// <summary>
    /// İptal edildi - Ödeme iptal edildi
    /// </summary>
    Cancelled = 4,

    /// <summary>
    /// İade edildi - Ödeme iade edildi
    /// </summary>
    Refunded = 5,

    /// <summary>
    /// Kısmi iade - Kısmi iade yapıldı
    /// </summary>
    PartialRefund = 6,

    /// <summary>
    /// İşleniyor - Ödeme işleniyor
    /// </summary>
    Processing = 7,

    /// <summary>
    /// Onay bekliyor - Ödeme onay bekliyor
    /// </summary>
    AwaitingConfirmation = 8
} 
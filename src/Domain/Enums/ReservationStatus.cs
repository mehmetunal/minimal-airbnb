namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Rezervasyon durumları
/// </summary>
public enum ReservationStatus
{
    /// <summary>
    /// Beklemede - Ev sahibi onayını bekliyor
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Onaylandı - Ev sahibi tarafından onaylandı
    /// </summary>
    Confirmed = 2,

    /// <summary>
    /// Reddedildi - Ev sahibi tarafından reddedildi
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// İptal edildi - Misafir tarafından iptal edildi
    /// </summary>
    Cancelled = 4,

    /// <summary>
    /// Tamamlandı - Rezervasyon süresi doldu
    /// </summary>
    Completed = 5,

    /// <summary>
    /// İptal edildi (Otomatik) - Sistem tarafından otomatik iptal
    /// </summary>
    AutoCancelled = 6,

    /// <summary>
    /// İptal edildi (Admin) - Admin tarafından iptal edildi
    /// </summary>
    AdminCancelled = 7
} 
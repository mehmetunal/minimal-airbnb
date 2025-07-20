namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Mesaj tipleri
/// </summary>
public enum MessageType
{
    /// <summary>
    /// Özel mesaj
    /// </summary>
    Private = 1,

    /// <summary>
    /// Sistem mesajı
    /// </summary>
    System = 2,

    /// <summary>
    /// Bildirim mesajı
    /// </summary>
    Notification = 3,

    /// <summary>
    /// Rezervasyon mesajı
    /// </summary>
    Reservation = 4,

    /// <summary>
    /// Destek mesajı
    /// </summary>
    Support = 5,

    /// <summary>
    /// Pazarlama mesajı
    /// </summary>
    Marketing = 6
} 
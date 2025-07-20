namespace MinimalAirbnb.Application.Common.Models;

/// <summary>
/// Standart API response modeli
/// </summary>
public class ApiResponse<T>
{
    /// <summary>
    /// İşlem başarılı mı?
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Response mesajı
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Response verisi
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Validation hataları
    /// </summary>
    public object? Errors { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

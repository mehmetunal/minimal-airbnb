namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Logging Servisi Interface
/// </summary>
public interface ILoggingService
{
    /// <summary>
    /// Information seviyesinde log
    /// </summary>
    void LogInformation(string message, params object[] args);

    /// <summary>
    /// Warning seviyesinde log
    /// </summary>
    void LogWarning(string message, params object[] args);

    /// <summary>
    /// Error seviyesinde log
    /// </summary>
    void LogError(string message, params object[] args);

    /// <summary>
    /// Error seviyesinde log (exception ile)
    /// </summary>
    void LogError(Exception exception, string message, params object[] args);

    /// <summary>
    /// Debug seviyesinde log
    /// </summary>
    void LogDebug(string message, params object[] args);

    /// <summary>
    /// Trace seviyesinde log
    /// </summary>
    void LogTrace(string message, params object[] args);

    /// <summary>
    /// Critical seviyesinde log
    /// </summary>
    void LogCritical(string message, params object[] args);

    /// <summary>
    /// Critical seviyesinde log (exception ile)
    /// </summary>
    void LogCritical(Exception exception, string message, params object[] args);
} 
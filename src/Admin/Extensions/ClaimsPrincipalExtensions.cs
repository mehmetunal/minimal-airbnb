using System.Security.Claims;

namespace MinimalAirbnb.Admin.Extensions;

/// <summary>
/// ClaimsPrincipal için extension method'lar
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Kullanıcı ID'sini alır
    /// </summary>
    public static string? GetUserId(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    /// <summary>
    /// Kullanıcı email'ini alır
    /// </summary>
    public static string? GetUserEmail(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.Email)?.Value;

    /// <summary>
    /// Kullanıcı adını alır
    /// </summary>
    public static string? GetUserName(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.Name)?.Value;

    /// <summary>
    /// Kullanıcı rolünü alır
    /// </summary>
    public static string? GetUserRole(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.Role)?.Value;

    /// <summary>
    /// Kullanıcı tipini alır
    /// </summary>
    public static string? GetUserType(this ClaimsPrincipal user)
        => user.FindFirst("UserType")?.Value;

    /// <summary>
    /// Kullanıcının belirli bir role sahip olup olmadığını kontrol eder
    /// </summary>
    public static bool HasRole(this ClaimsPrincipal user, string role)
        => user.IsInRole(role);

    /// <summary>
    /// Kullanıcının admin olup olmadığını kontrol eder
    /// </summary>
    public static bool IsAdmin(this ClaimsPrincipal user)
        => user.HasRole("Admin");

    /// <summary>
    /// Kullanıcının host olup olmadığını kontrol eder
    /// </summary>
    public static bool IsHost(this ClaimsPrincipal user)
        => user.GetUserType() == "Host";

    /// <summary>
    /// Kullanıcının guest olup olmadığını kontrol eder
    /// </summary>
    public static bool IsGuest(this ClaimsPrincipal user)
        => user.GetUserType() == "Guest";
} 
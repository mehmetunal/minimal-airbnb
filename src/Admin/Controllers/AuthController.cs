using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Microsoft.AspNetCore.Authorization;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Authentication Controller
/// </summary>
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AuthController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Admin login sayfası
    /// </summary>
    [HttpGet("Login")]
    public IActionResult Login()
    {
        // Eğer zaten giriş yapmışsa dashboard'a yönlendir
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    /// <summary>
    /// Admin login işlemi
    /// </summary>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AdminLoginRequest request)
    {
        try
        {
            var loginData = new
            {
                Email = request.Email,
                Password = request.Password,
                RememberMe = request.RememberMe
            };

            var response = await _httpClient.PostAsync<AdminLoginResponseDto>("/api/auth/login", loginData);

            if (response is { IsSuccess: true, Data: not null })
            {
                // Admin kontrolü - sadece admin kullanıcılar giriş yapabilir
                if (response.Data.UserType != "Admin")
                {
                    return Json(new { success = false, message = "Bu panele sadece admin kullanıcılar erişebilir." });
                }

                // Session kullanmıyoruz - bilgiler Claims içinde saklanıyor

                // Cookie authentication oluştur
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, response.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email, response.Data.Email),
                    new Claim(ClaimTypes.Name, response.Data.FullName),
                    new Claim(ClaimTypes.Role, response.Data.UserType),
                    new Claim("UserType", response.Data.UserType)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = request.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Json(new { success = true, message = "Admin paneline başarıyla giriş yaptınız!", data = response.Data, redirectUrl = "/Home/Index" });
            }
            else
            {
                return Json(new { success = false, message = response?.Message ?? "Giriş başarısız." });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Giriş yapılırken bir hata oluştu: " + ex.Message });
        }
    }

    /// <summary>
    /// Admin logout işlemi
    /// </summary>
    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Json(new { success = true, message = "Başarıyla çıkış yaptınız!", redirectUrl = "/Auth/Login" });
    }

    /// <summary>
    /// Admin profil sayfası
    /// </summary>
    [HttpGet("Profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        try
        {
            // Current admin'dan UserId'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return RedirectToAction("Login");
            }

            var response = await _httpClient.GetAsync<Result<AdminUserDto>>($"/api/users/{userId}");

            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Profil bilgileri yüklenirken bir hata oluştu.");
        }

        return View();
    }

    /// <summary>
    /// Erişim reddedildi sayfası
    /// </summary>
    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}

/// <summary>
/// Admin login isteği
/// </summary>
public class AdminLoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}

/// <summary>
/// Admin login response DTO'su
/// </summary>
public class AdminLoginResponseDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
}

/// <summary>
/// Admin user DTO'su
/// </summary>
public class AdminUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string UserType { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MinimalAirbnb.Application.Users.Queries.GetUserByEmail;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Microsoft.AspNetCore.Authorization;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Users Web Controller
/// </summary>
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public UsersController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Login sayfası
    /// </summary>
    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Register sayfası
    /// </summary>
    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Login işlemi
    /// </summary>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var loginData = new
            {
                Email = request.Email,
                Password = request.Password,
                RememberMe = request.RememberMe
            };

            var response = await _httpClient.PostAsync<LoginResponseDto>("/api/auth/login", loginData);

            if (response != null && response.IsSuccess && response.Data != null)
            {
                // Session'a kullanıcı bilgilerini kaydet
                HttpContext.Session.SetString("UserId", response.Data.UserId.ToString());
                HttpContext.Session.SetString("UserEmail", response.Data.Email);
                HttpContext.Session.SetString("UserName", response.Data.FullName);

                // Cookie authentication oluştur
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, response.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email, response.Data.Email),
                    new Claim(ClaimTypes.Name, response.Data.FullName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = request.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Json(new { success = true, message = "Başarıyla giriş yaptınız!", data = response.Data, redirectUrl = "/Users/Dashboard" });
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
    /// Register işlemi
    /// </summary>
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var registerData = new
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode
            };

            var response = await _httpClient.PostAsync<Result<RegisterResponseDto>>("/api/auth/register", registerData);

            if (response != null && response.IsSuccess && response.Data != null)
            {
                return Json(new { success = true, message = "Başarıyla kayıt oldunuz! Şimdi giriş yapabilirsiniz.", data = response.Data, redirectUrl = "/Users/Login" });
            }
            else
            {
                return Json(new { success = false, message = response?.Message ?? "Kayıt başarısız." });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Kayıt olurken bir hata oluştu: " + ex.Message });
        }
    }

    /// <summary>
    /// Logout işlemi
    /// </summary>
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Json(new { success = true, message = "Başarıyla çıkış yaptınız!" });
    }

    /// <summary>
    /// Kullanıcı dashboard'u
    /// </summary>
    [HttpGet("Dashboard")]
    [Authorize]
    public async Task<IActionResult> Dashboard()
    {
        try
        {
            // Current user'dan UserId'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return RedirectToAction("Login");
            }

            // Kullanıcı bilgilerini al
            var userResponse = await _httpClient.GetAsync<Result<UserDto>>($"/api/users/{userId}");
            var user = userResponse?.Data;

            ViewBag.User = user;
            return View();
        }
        catch
        {
            return RedirectToAction("Login");
        }
    }

    /// <summary>
    /// Profil sayfası
    /// </summary>
    [HttpGet("Profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        try
        {
            // Current user'dan UserId'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return RedirectToAction("Login");
            }

            var response = await _httpClient.GetAsync<Result<UserDto>>($"/api/users/{userId}");

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
}

/// <summary>
/// Login isteği
/// </summary>
public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}

/// <summary>
/// Register isteği
/// </summary>
public class RegisterRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
}

/// <summary>
/// Login response DTO'su
/// </summary>
public class LoginResponseDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}

/// <summary>
/// Register response DTO'su
/// </summary>
public class RegisterResponseDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}

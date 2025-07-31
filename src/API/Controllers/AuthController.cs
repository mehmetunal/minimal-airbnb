using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Users.Queries.GetUserByEmail;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using Microsoft.AspNetCore.Identity;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Authentication API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(
        IMediator mediator,
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _mediator = mediator;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Kullanıcı girişi
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<Result<LoginResponseDto>>> Login([FromBody] LoginRequest request)
    {
        try
        {
            // Kullanıcıyı e-posta ile bul
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(Result<LoginResponseDto>.Failure(new Error("400", "Geçersiz e-posta veya şifre.")));
            }

            // Şifreyi kontrol et
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest(Result<LoginResponseDto>.Failure(new Error("400", "Geçersiz e-posta veya şifre.")));
            }

            // Session'a giriş yap
            await _signInManager.SignInAsync(user, request.RememberMe);

            var response = new LoginResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                UserType = user.UserType.ToString()
            };

            return Ok(Result<LoginResponseDto>.Success(response, new SuccessMessage("200", "Başarıyla giriş yaptınız.")));
        }
        catch (Exception ex)
        {
            return BadRequest(Result<LoginResponseDto>.Failure(new Error("500", $"Giriş yapılırken hata oluştu: {ex.Message}")));
        }
    }

    /// <summary>
    /// Kullanıcı kaydı
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<Result<RegisterResponseDto>>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            // E-posta kontrolü
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest(Result<RegisterResponseDto>.Failure(new Error("400", "Bu e-posta adresi zaten kullanılıyor.")));
            }

            // Şifre kontrolü
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(Result<RegisterResponseDto>.Failure(new Error("400", "Şifreler eşleşmiyor.")));
            }

            // Yeni kullanıcı oluştur
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode,
                EmailConfirmed = true, // Demo için otomatik onay
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(Result<RegisterResponseDto>.Failure(new Error("400", $"Kayıt başarısız: {errors}")));
            }

            var response = new RegisterResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}"
            };

            return Ok(Result<RegisterResponseDto>.Success(response, new SuccessMessage("200", "Başarıyla kayıt oldunuz.")));
        }
        catch (Exception ex)
        {
            return BadRequest(Result<RegisterResponseDto>.Failure(new Error("500", $"Kayıt olurken hata oluştu: {ex.Message}")));
        }
    }

    /// <summary>
    /// Kullanıcı çıkışı
    /// </summary>
    [HttpPost("logout")]
    public async Task<ActionResult<Result<object>>> Logout()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return Ok(Result<object>.Success(null, new SuccessMessage("200", "Başarıyla çıkış yaptınız.")));
        }
        catch (Exception ex)
        {
            return BadRequest(Result<object>.Failure(new Error("500", $"Çıkış yapılırken hata oluştu: {ex.Message}")));
        }
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
    public string UserType { get; set; } = string.Empty;
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

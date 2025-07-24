using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Properties.Queries.GetProperties;
using MinimalAirbnb.Application.Properties.Queries.GetPropertyById;
using MinimalAirbnb.Application.Properties.DTOs;
using MinimalAirbnb.Application.Reservations.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Web.Models;
using System.Security.Claims;
using MinimalAirbnb.Application.Favorites.DTOs;
using MinimalAirbnb.Application.Payments.DTOs;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Properties Web Controller
/// </summary>
[Route("[controller]")]
public class PropertiesController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PropertiesController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Properties listesini göster
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetPropertiesQuery query)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedListWrapper<PropertyDto>>($"/api/properties?PageNumber={query.PageNumber}&PageSize={query.PageSize}&City={query.City}&MinPrice={query.MinPrice}&MaxPrice={query.MaxPrice}&PropertyType={query.PropertyType}");

            if (response is { Data: not null })
            {
                return View(response);
            }
        }
        catch
        {
            // Log error
            ModelState.AddModelError("", "Properties yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<PropertyDto>.Empty(query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Property detayını göster
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<PropertyDto>>($"/api/properties/{id}");

            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            // Log error
            ModelState.AddModelError("", "Property detayları yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Property arama sayfası
    /// </summary>
    [HttpGet("Search")]
    public IActionResult Search()
    {
        return View();
    }

    /// <summary>
    /// Yeni property oluşturma sayfası
    /// </summary>
    [HttpGet("Create")]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Rezervasyon yap
    /// </summary>
    [HttpPost("CreateReservation/{propertyId}")]
    [Authorize]
    public async Task<IActionResult> CreateReservation(Guid propertyId, [FromBody] CreateReservationRequest request)
    {
        try
        {
            // Current user'dan UserId'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Json(new { success = false, message = "Kullanıcı bilgisi bulunamadı." });
            }

            // 1. Önce rezervasyon oluştur
            var reservationCommand = new
            {
                PropertyId = propertyId,
                UserId = userId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                GuestCount = request.GuestCount,
                TotalPrice = request.TotalPrice,
                Notes = request.Notes
            };

            var reservationResponse = await _httpClient.PostAsync<CreateReservationResponseDto>("/api/reservations", reservationCommand);
            if (reservationResponse == null || !reservationResponse.IsSuccess || reservationResponse.Data == null)
            {
                return Json(new { success = false, message = reservationResponse?.Message ?? "Rezervasyon oluşturulurken bir hata oluştu." });
            }

            // 2. Ödeme oluştur
            var paymentCommand = new
            {
                ReservationId = reservationResponse.Data.ReservationId,
                UserId = userId,
                Amount = request.TotalPrice,
                Currency = "TRY",
                PaymentMethod = request.PaymentMethod,
                CardNumber = request.CardNumber,
                CardHolderName = request.CardHolderName,
                ExpiryDate = request.ExpiryDate,
                CVV = request.CVV,
                Description = $"Rezervasyon ödemesi - Property ID: {propertyId}",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            };

            var paymentResponse = await _httpClient.PostAsync<CreatePaymentResponseDto>("/api/payments", paymentCommand);
            if (paymentResponse == null || !paymentResponse.IsSuccess)
            {
                // Ödeme başarısız olursa rezervasyonu iptal et
                if (reservationResponse.Data.ReservationId != Guid.Empty)
                {
                    await _httpClient.DeleteAsync($"/api/reservations/{reservationResponse.Data.ReservationId}", reservationResponse.Data.ReservationId);
                }
                return Json(new { success = false, message = paymentResponse?.Message ?? "Ödeme işlemi başarısız." });
            }

            return Json(new {
                success = true,
                message = "Rezervasyon ve ödeme başarıyla tamamlandı.",
                data = new {
                    reservation = reservationResponse.Data,
                    payment = paymentResponse.Data
                }
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "İşlem sırasında bir hata oluştu: " + ex.Message });
        }
    }

    /// <summary>
    /// Favorilere ekle/çıkar
    /// </summary>
    [HttpPost("ToggleFavorite/{propertyId}")]
    [Authorize]
    public async Task<IActionResult> ToggleFavorite(Guid propertyId)
    {
        try
        {
            // Current user'dan UserId'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Json(new { success = false, message = "Kullanıcı bilgisi bulunamadı." });
            }

            var favoriteCommand = new
            {
                PropertyId = propertyId,
                UserId = userId
            };

            var response = await _httpClient.PostAsync<PagedListWrapper<FavoriteDto>>("/api/favorites", favoriteCommand);

            if (response != null && response.IsSuccess)
            {
                return Json(new { success = true, message = "Favori işlemi başarılı.", data = response.Data });
            }
            else
            {
                return Json(new { success = false, message = response?.Message ?? "Favori işlemi başarısız." });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Favori işlemi sırasında hata: " + ex.Message });
        }
    }
}

/// <summary>
/// Rezervasyon oluşturma isteği
/// </summary>
public class CreateReservationRequest
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int GuestCount { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Notes { get; set; }

    // Ödeme bilgileri
    public string PaymentMethod { get; set; } = string.Empty; // "CreditCard", "DebitCard", "PayPal" vb.
    public string CardNumber { get; set; } = string.Empty;
    public string ExpiryDate { get; set; } = string.Empty; // "MM/YY" formatında
    public string CVV { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
}

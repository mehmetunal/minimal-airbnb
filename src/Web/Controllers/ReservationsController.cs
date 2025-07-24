using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MinimalAirbnb.Application.Reservations.Queries.GetReservations;
using MinimalAirbnb.Application.Reservations.Queries.GetReservationById;
using MinimalAirbnb.Application.Reservations.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Web.Models;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Reservations Web Controller
/// </summary>
[Authorize]
public class ReservationsController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ReservationsController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Rezervasyon listesini göster
    /// </summary>
    public async Task<IActionResult> Index([FromQuery] GetReservationsQuery query)
    {
        try
        {
            var startDateParam = query.StartDate.HasValue ? $"&StartDate={query.StartDate.Value:yyyy-MM-dd}" : "";
            var endDateParam = query.EndDate.HasValue ? $"&EndDate={query.EndDate.Value:yyyy-MM-dd}" : "";

            var response = await _httpClient.GetAsync<PagedListWrapper<ReservationDto>>($"/api/reservations?PageNumber={query.PageNumber}&PageSize={query.PageSize}&UserId={query.UserId}&PropertyId={query.PropertyId}{startDateParam}{endDateParam}");

            if (response is { Data: not null })
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Rezervasyonlar yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<ReservationDto>.Empty(query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Rezervasyon detayını göster
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<ReservationDto>>($"/api/reservations/{id}");

            if (response is { IsSuccess: true, Data: not null })
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Rezervasyon detayları yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Yeni rezervasyon oluşturma sayfası
    /// </summary>
    public IActionResult Create(Guid propertyId)
    {
        ViewBag.PropertyId = propertyId;
        return View();
    }

    /// <summary>
    /// Rezervasyon düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<ReservationDto>>($"/api/reservations/{id}");

            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Rezervasyon bilgileri yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Kullanıcının rezervasyonlarını göster
    /// </summary>
    public async Task<IActionResult> MyReservations([FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            // Claims'den UserId'yi al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                return RedirectToAction("Login", "Users");
            }

            var response = await _httpClient.GetAsync<PagedListWrapper<ReservationDto>>($"/api/reservations?UserId={parsedUserId}&PageNumber={pageNumber}&PageSize={pageSize}");

            if (response is { Data: not null })
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Rezervasyonlarınız yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<ReservationDto>.Empty(pageNumber, pageSize));
    }
}

using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Reservations.Queries.GetReservations;
using MinimalAirbnb.Application.Reservations.Queries.GetReservationById;
using MinimalAirbnb.Application.Reservations.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Reservations Web Controller
/// </summary>
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
            var response = await _httpClient.GetAsync<PagedList<ReservationDto>>($"/api/reservations?PageNumber={query.PageNumber}&PageSize={query.PageSize}&UserId={query.UserId}&PropertyId={query.PropertyId}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Rezervasyonlar yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<ReservationDto>(new List<ReservationDto>(), 0, query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Rezervasyon detayını göster
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
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
    public async Task<IActionResult> MyReservations(Guid userId, [FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<ReservationDto>>($"/api/reservations?UserId={userId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Rezervasyonlarınız yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<ReservationDto>(new List<ReservationDto>(), 0, pageNumber, pageSize));
    }
} 
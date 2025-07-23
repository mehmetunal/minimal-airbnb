using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Reviews.Queries.GetReviews;
using MinimalAirbnb.Application.Reviews.Queries.GetReviewById;
using MinimalAirbnb.Application.Reviews.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Reviews Web Controller
/// </summary>
public class ReviewsController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ReviewsController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Değerlendirme listesini göster
    /// </summary>
    public async Task<IActionResult> Index([FromQuery] GetReviewsQuery query)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<ReviewDto>>($"/api/reviews?PageNumber={query.PageNumber}&PageSize={query.PageSize}&PropertyId={query.PropertyId}&UserId={query.UserId}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Değerlendirmeler yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<ReviewDto>(new List<ReviewDto>(), 0, query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Değerlendirme detayını göster
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<ReviewDto>>($"/api/reviews/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Değerlendirme detayları yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Yeni değerlendirme oluşturma sayfası
    /// </summary>
    public IActionResult Create(Guid propertyId, Guid? reservationId = null)
    {
        ViewBag.PropertyId = propertyId;
        ViewBag.ReservationId = reservationId;
        return View();
    }

    /// <summary>
    /// Değerlendirme düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<ReviewDto>>($"/api/reviews/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Değerlendirme bilgileri yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Property'ye ait değerlendirmeleri göster
    /// </summary>
    public async Task<IActionResult> PropertyReviews(Guid propertyId, [FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<ReviewDto>>($"/api/reviews?PropertyId={propertyId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Property değerlendirmeleri yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<ReviewDto>(new List<ReviewDto>(), 0, pageNumber, pageSize));
    }

    /// <summary>
    /// Kullanıcının değerlendirmelerini göster
    /// </summary>
    public async Task<IActionResult> MyReviews(Guid userId, [FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<ReviewDto>>($"/api/reviews?UserId={userId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Değerlendirmeleriniz yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<ReviewDto>(new List<ReviewDto>(), 0, pageNumber, pageSize));
    }
} 
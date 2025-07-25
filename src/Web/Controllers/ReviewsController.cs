using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Reviews.Queries.GetReviews;
using MinimalAirbnb.Application.Reviews.Queries.GetReviewById;
using MinimalAirbnb.Application.Reviews.Commands.CreateReview;
using MinimalAirbnb.Application.Reviews.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Web.Models;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Reviews Web Controller
/// </summary>
[Authorize]
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
    public async Task<IActionResult> Index([FromQuery] GetReviewsQuery query,
                [FromQuery] int? Rating,
                [FromQuery] bool? IsApproved,
                [FromQuery] DateTime? StartDate,
                [FromQuery] DateTime? EndDate)
    {
        try
        {
            var queryParams = new List<string>
            {
                $"PageNumber={query.PageNumber}",
                $"PageSize={query.PageSize}"
            };

            if (query.PropertyId.HasValue)
                queryParams.Add($"PropertyId={query.PropertyId}");

            if (query.UserId.HasValue)
                queryParams.Add($"UserId={query.UserId}");

            if (Rating.HasValue)
                queryParams.Add($"Rating={Rating}");

            if (IsApproved.HasValue)
                queryParams.Add($"IsApproved={IsApproved}");

            if (StartDate.HasValue)
                queryParams.Add($"StartDate={StartDate:yyyy-MM-dd}");

            if (EndDate.HasValue)
                queryParams.Add($"EndDate={EndDate:yyyy-MM-dd}");

            var queryString = string.Join("&", queryParams);
            var response = await _httpClient.GetAsync<PagedListWrapper<ReviewDto>>($"/api/reviews?{queryString}");

            if (response != null)
            {
                ViewBag.Rating = Rating;
                ViewBag.IsApproved = IsApproved;
                ViewBag.StartDate = StartDate;
                ViewBag.EndDate = EndDate;
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Değerlendirmeler yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<ReviewDto>.Empty(query.PageNumber, query.PageSize));
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
    /// Yeni değerlendirme oluştur
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewRequest request)
    {
        try
        {
            var command = new CreateReviewCommand
            {
                PropertyId = request.PropertyId,
                ReservationId = request.ReservationId,
                UserId = request.UserId,
                Rating = request.Rating,
                Comment = request.Comment,
                CleanlinessRating = request.CleanlinessRating,
                CommunicationRating = request.CommunicationRating,
                CheckInRating = request.CheckInRating,
                AccuracyRating = request.AccuracyRating,
                LocationRating = request.LocationRating,
                ValueRating = request.ValueRating
            };

            var response = await _httpClient.PostAsync<object>("/api/reviews", command);

            if (response is { IsSuccess: true })
            {
                return Json(new { success = true, message = "Değerlendirme başarıyla oluşturuldu!" });
            }
            else
            {
                return Json(new { success = false, message = response?.Message ?? "Değerlendirme oluşturulurken bir hata oluştu." });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Değerlendirme oluşturulurken bir hata oluştu: " + ex.Message });
        }
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
            var response = await _httpClient.GetAsync<PagedListWrapper<ReviewDto>>($"/api/reviews?PropertyId={propertyId}&PageNumber={pageNumber}&PageSize={pageSize}");

            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Property değerlendirmeleri yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<ReviewDto>.Empty(pageNumber, pageSize));
    }

    /// <summary>
    /// Kullanıcının değerlendirmelerini göster
    /// </summary>
    public async Task<IActionResult> MyReviews(Guid userId, [FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedListWrapper<ReviewDto>>($"/api/reviews?UserId={userId}&PageNumber={pageNumber}&PageSize={pageSize}");

            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Değerlendirmeleriniz yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<ReviewDto>.Empty(pageNumber, pageSize));
    }
}

/// <summary>
/// Review oluşturma isteği
/// </summary>
public class CreateReviewRequest
{
    public Guid PropertyId { get; set; }
    public Guid? ReservationId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int? CleanlinessRating { get; set; }
    public int? CommunicationRating { get; set; }
    public int? CheckInRating { get; set; }
    public int? AccuracyRating { get; set; }
    public int? LocationRating { get; set; }
    public int? ValueRating { get; set; }
}

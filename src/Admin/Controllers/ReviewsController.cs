using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Reviews.Queries.GetReviews;
using MinimalAirbnb.Application.Reviews.Queries.GetReviewById;
using MinimalAirbnb.Application.Reviews.DTOs;
using MinimalAirbnb.Application.Commands.Review;

using MinimalAirbnb.Application.Reviews.Commands.DeleteReview;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Admin.Models;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Reviews Controller
/// </summary>
[Authorize(Roles = "Admin")]
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
    /// Yorumları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedListWrapper<ReviewDto>>($"/api/reviews?PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Yorumlar yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<ReviewDto>.Empty(pageNumber, pageSize));
    }

    /// <summary>
    /// Yorum detayı
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
        catch (Exception ex)
        {
            TempData["Error"] = "Yorum bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Yorum oluşturma sayfası
    /// </summary>
    public IActionResult Create()
    {
        return View(new CreateReviewCommand());
    }

    /// <summary>
    /// Yorum oluştur
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateReviewCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            var response = await _httpClient.PostAsync<Result<object>>("/api/reviews", command);
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Yorum başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Yorum oluşturulurken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Yorum oluşturulurken hata oluştu.";
        }

        return View(command);
    }

    /// <summary>
    /// Yorum düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<ReviewDto>>($"/api/reviews/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                var updateCommand = new MinimalAirbnb.Application.Reviews.Commands.UpdateReview.UpdateReviewCommand
                {
                    Id = response.Data.Id,
                    Rating = (int)response.Data.Rating,
                    Comment = response.Data.Comment
                };
                return View(updateCommand);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Yorum bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Yorum düzenleme işlemi
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MinimalAirbnb.Application.Reviews.Commands.UpdateReview.UpdateReviewCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            var response = await _httpClient.PutAsync<Result<object>>($"/api/reviews/{command.Id}", command);
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Yorum başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Yorum güncellenirken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Yorum güncellenirken hata oluştu.";
        }

        return View(command);
    }

    /// <summary>
    /// Yorum silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<ReviewDto>>($"/api/reviews/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Yorum bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Yorum silme işlemi
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/reviews/{id}", new { id });
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Yorum başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Yorum silinirken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Yorum silinirken hata oluştu.";
        }

        return RedirectToAction(nameof(Index));
    }
} 
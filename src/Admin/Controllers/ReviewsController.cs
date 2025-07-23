using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Reviews.Queries.GetReviews;
using MinimalAirbnb.Application.Reviews.Queries.GetReviewById;
using MediatR;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Reviews Controller
/// </summary>
public class ReviewsController : Controller
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Yorumları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var query = new GetReviewsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return View(result);
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Yorumlar yüklenirken bir hata oluştu.";
            return View(new PagedList<MinimalAirbnb.Application.Reviews.DTOs.ReviewDto>(new List<MinimalAirbnb.Application.Reviews.DTOs.ReviewDto>(), 0, pageSize, 0));
        }
    }

    /// <summary>
    /// Yorum detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var query = new GetReviewByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                return View(result.Data);
            }

            TempData["Error"] = "Yorum bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Yorum detayları yüklenirken bir hata oluştu.";
            return RedirectToAction(nameof(Index));
        }
    }
} 
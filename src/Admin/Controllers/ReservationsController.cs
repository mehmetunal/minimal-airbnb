using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Reservations.Queries.GetReservations;
using MinimalAirbnb.Application.Reservations.Queries.GetReservationById;
using MediatR;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Reservations Controller
/// </summary>
public class ReservationsController : Controller
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Rezervasyonları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var query = new GetReservationsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return View(result);
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyonlar yüklenirken bir hata oluştu.";
            return View(new PagedList<MinimalAirbnb.Application.Reservations.DTOs.ReservationDto>(new List<MinimalAirbnb.Application.Reservations.DTOs.ReservationDto>(), 0, pageSize, 0));
        }
    }

    /// <summary>
    /// Rezervasyon detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var query = new GetReservationByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                return View(result.Data);
            }

            TempData["Error"] = "Rezervasyon bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyon detayları yüklenirken bir hata oluştu.";
            return RedirectToAction(nameof(Index));
        }
    }
} 
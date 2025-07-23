using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Payments.Queries.GetPayments;
using MinimalAirbnb.Application.Payments.Queries.GetPaymentById;
using MediatR;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Payments Controller
/// </summary>
public class PaymentsController : Controller
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Ödemeleri listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var query = new GetPaymentsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return View(result);
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödemeler yüklenirken bir hata oluştu.";
            return View(new PagedList<MinimalAirbnb.Application.Payments.DTOs.PaymentDto>(new List<MinimalAirbnb.Application.Payments.DTOs.PaymentDto>(), 0, pageSize, 0));
        }
    }

    /// <summary>
    /// Ödeme detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var query = new GetPaymentByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                return View(result.Data);
            }

            TempData["Error"] = "Ödeme bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödeme detayları yüklenirken bir hata oluştu.";
            return RedirectToAction(nameof(Index));
        }
    }
} 
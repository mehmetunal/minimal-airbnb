using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Payments.Queries.GetPayments;
using MinimalAirbnb.Application.Payments.Queries.GetPaymentById;
using MinimalAirbnb.Application.Payments.DTOs;
using MinimalAirbnb.Application.Commands.Payment;

using MinimalAirbnb.Application.Payments.Commands.DeletePayment;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Admin.Models;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Payments Controller
/// </summary>
[Authorize(Roles = "Admin")]
public class PaymentsController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PaymentsController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Ödemeleri listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedListWrapper<PaymentDto>>($"/api/payments?PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Ödemeler yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<PaymentDto>.Empty(pageNumber, pageSize));
    }

    /// <summary>
    /// Ödeme detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<PaymentDto>>($"/api/payments/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödeme bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Ödeme oluşturma sayfası
    /// </summary>
    public IActionResult Create()
    {
        return View(new CreatePaymentCommand());
    }

    /// <summary>
    /// Ödeme oluştur
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePaymentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            var response = await _httpClient.PostAsync<Result<object>>("/api/payments", command);
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Ödeme başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Ödeme oluşturulurken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödeme oluşturulurken hata oluştu.";
        }

        return View(command);
    }

    /// <summary>
    /// Ödeme düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<PaymentDto>>($"/api/payments/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                var updateCommand = new MinimalAirbnb.Application.Payments.Commands.UpdatePayment.UpdatePaymentCommand
                {
                    Id = response.Data.Id,
                    Amount = response.Data.Amount,
                    PaymentMethod = response.Data.PaymentMethod,
                    Status = response.Data.Status,
                    Notes = response.Data.Description
                };
                return View(updateCommand);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödeme bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Ödeme düzenleme işlemi
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MinimalAirbnb.Application.Payments.Commands.UpdatePayment.UpdatePaymentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            var response = await _httpClient.PutAsync<Result<object>>($"/api/payments/{command.Id}", command);
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Ödeme başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Ödeme güncellenirken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödeme güncellenirken hata oluştu.";
        }

        return View(command);
    }

    /// <summary>
    /// Ödeme silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<PaymentDto>>($"/api/payments/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödeme bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Ödeme silme işlemi
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/payments/{id}", new { id });
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Ödeme başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Ödeme silinirken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ödeme silinirken hata oluştu.";
        }

        return RedirectToAction(nameof(Index));
    }
} 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Reservations.Queries.GetReservations;
using MinimalAirbnb.Application.Reservations.Queries.GetReservationById;
using MinimalAirbnb.Application.Reservations.DTOs;
using MinimalAirbnb.Application.Commands.Reservation;

using MinimalAirbnb.Application.Reservations.Commands.DeleteReservation;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Admin.Models;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Reservations Controller
/// </summary>
[Authorize(Roles = "Admin")]
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
    /// Rezervasyonları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedListWrapper<ReservationDto>>($"/api/reservations?PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Rezervasyonlar yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<ReservationDto>.Empty(pageNumber, pageSize));
    }

    /// <summary>
    /// Rezervasyon detayı
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
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyon bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Rezervasyon oluşturma sayfası
    /// </summary>
    public IActionResult Create()
    {
        return View(new CreateReservationCommand());
    }

    /// <summary>
    /// Rezervasyon oluştur
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateReservationCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            var response = await _httpClient.PostAsync<Result<object>>("/api/reservations", command);
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Rezervasyon başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Rezervasyon oluşturulurken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyon oluşturulurken hata oluştu.";
        }

        return View(command);
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
                var updateCommand = new MinimalAirbnb.Application.Reservations.Commands.UpdateReservation.UpdateReservationCommand
                {
                    ReservationId = response.Data.Id,
                    CheckInDate = response.Data.CheckInDate,
                    CheckOutDate = response.Data.CheckOutDate,
                    TotalPrice = response.Data.TotalPrice,
                    Status = response.Data.Status
                };
                return View(updateCommand);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyon bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Rezervasyon düzenleme işlemi
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MinimalAirbnb.Application.Reservations.Commands.UpdateReservation.UpdateReservationCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            var response = await _httpClient.PutAsync<Result<object>>($"/api/reservations/{command.ReservationId}", command);
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Rezervasyon başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Rezervasyon güncellenirken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyon güncellenirken hata oluştu.";
        }

        return View(command);
    }

    /// <summary>
    /// Rezervasyon silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<ReservationDto>>($"/api/reservations/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyon bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Rezervasyon silme işlemi
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/reservations/{id}", new { id });
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Rezervasyon başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Rezervasyon silinirken hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Rezervasyon silinirken hata oluştu.";
        }

        return RedirectToAction(nameof(Index));
    }
} 
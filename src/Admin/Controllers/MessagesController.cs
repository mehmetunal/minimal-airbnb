using Maggsoft.Core.Base;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Admin.Models;
using Maggsoft.Framework.HttpClientApi;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Messages.DTOs;
using MinimalAirbnb.Application.Commands.Message;


namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Messages Controller
/// </summary>
[Authorize(Roles = "Admin")]
public class MessagesController(IMaggsoftHttpClient httpClient)
    : Controller
{
    /// <summary>
    /// Mesajları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await httpClient.GetAsync<PagedListWrapper<MessageDto>>($"/api/messages?PageNumber={pageNumber}&PageSize={pageSize}");

            if (response != null)
            {
                return View(response);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Mesajlar yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<MessageDto>.Empty(pageNumber, pageSize));
    }

    /// <summary>
    /// Mesaj detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await httpClient.GetAsync<Result<MessageDto>>($"/api/messages/{id}");

            if (response is { IsSuccess: true, Data: not null })
            {
                return View(response.Data);
            }
        }
        catch
        {
            TempData["Error"] = "Mesaj bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Mesaj silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await httpClient.GetAsync<Result<MessageDto>>($"/api/messages/{id}");

            if (response is { IsSuccess: true, Data: not null })
            {
                return View(response.Data);
            }
        }
        catch
        {
            TempData["Error"] = "Mesaj bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Mesaj silme işlemi
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"/api/messages/{id}", new { id });

            if (response is { IsSuccess: true })
            {
                TempData["Success"] = "Mesaj başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Mesaj silinirken hata oluştu.";
            }
        }
        catch
        {
            TempData["Error"] = "Mesaj silinirken hata oluştu.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Mesaj düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await httpClient.GetAsync<Result<MessageDto>>($"/api/messages/{id}");

            if (response is { IsSuccess: true, Data: not null })
            {
                // MessageDto'yu UpdateMessageCommand'e dönüştür
                var updateCommand = new MinimalAirbnb.Application.Messages.Commands.UpdateMessage.UpdateMessageCommand
                {
                    Id = response.Data.Id,
                    Subject = response.Data.Subject,
                    Content = response.Data.Content,
                    IsRead = response.Data.IsRead,
                    IsArchived = response.Data.IsArchived
                };

                return View(updateCommand);
            }
        }
        catch
        {
            TempData["Error"] = "Mesaj bulunamadı.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Mesaj düzenleme işlemi
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MinimalAirbnb.Application.Messages.Commands.UpdateMessage.UpdateMessageCommand command)
    {
        if (!ModelState.IsValid)
        {
            // ModelState geçersizse, mesajı tekrar yükle
            var reloadResponse = await httpClient.GetAsync<Result<MessageDto>>($"/api/messages/{id}");
            if (reloadResponse is { IsSuccess: true, Data: not null })
            {
                // MessageDto'yu UpdateMessageCommand'e dönüştür
                var updateCommand = new MinimalAirbnb.Application.Messages.Commands.UpdateMessage.UpdateMessageCommand
                {
                    Id = reloadResponse.Data.Id,
                    Subject = reloadResponse.Data.Subject,
                    Content = reloadResponse.Data.Content,
                    IsRead = reloadResponse.Data.IsRead,
                    IsArchived = reloadResponse.Data.IsArchived
                };

                return View(updateCommand);
            }
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var response = await httpClient.PutAsync<Result<object>>($"/api/messages/{id}", command);

            if (response is { IsSuccess: true })
            {
                TempData["Success"] = "Mesaj başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = response?.Message ?? "Mesaj güncellenirken hata oluştu.";
        }
        catch
        {
            TempData["Error"] = "Mesaj güncellenirken hata oluştu.";
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Yeni mesaj oluşturma sayfası
    /// </summary>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Yeni mesaj oluşturma işlemi
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMessageCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            var response = await httpClient.PostAsync<Result<object>>($"/api/messages", command);

            if (response is { IsSuccess: true })
            {
                TempData["Success"] = "Mesaj başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.Message ?? "Mesaj oluşturulurken hata oluştu.";
            }
        }
        catch
        {
            TempData["Error"] = "Mesaj oluşturulurken hata oluştu.";
        }

        return View(command);
    }
}

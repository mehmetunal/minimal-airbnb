using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Messages.Queries.GetMessages;
using MinimalAirbnb.Application.Messages.Queries.GetMessageById;
using MinimalAirbnb.Application.Commands.Message;
using MediatR;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Messages Controller
/// </summary>
public class MessagesController : Controller
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Mesajları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var query = new GetMessagesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return View(result);
    }

    /// <summary>
    /// Mesaj detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        var query = new GetMessageByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
        {
            TempData["Error"] = "Mesaj bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        return View(result);
    }

    /// <summary>
    /// Mesaj silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        var query = new GetMessageByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
        {
            TempData["Error"] = "Mesaj bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        return View(result);
    }

    /// <summary>
    /// Mesaj silme işlemi
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var command = new DeleteMessageCommand { Id = id };
        var result = await _mediator.Send(command);
        if (result)
        {
            TempData["Success"] = "Mesaj başarıyla silindi.";
        }
        else
        {
            TempData["Error"] = "Mesaj silinirken hata oluştu.";
        }
        return RedirectToAction(nameof(Index));
    }
} 
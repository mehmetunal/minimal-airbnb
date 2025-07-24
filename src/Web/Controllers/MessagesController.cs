using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Messages.Queries.GetMessages;
using MinimalAirbnb.Application.Messages.Queries.GetMessageById;
using MinimalAirbnb.Application.Messages.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Web.Models;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Messages Web Controller
/// </summary>
[Authorize]
public class MessagesController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MessagesController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Mesaj listesini göster
    /// </summary>
    public async Task<IActionResult> Index([FromQuery] GetMessagesQuery query)
    {
        try
        {
            // Session'dan UserId'yi al
            var userId = HttpContext.Session.GetString("UserId");
            var currentUserId = !string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var parsedUserId) ? parsedUserId.ToString() : query.SenderId?.ToString();

            var response = await _httpClient.GetAsync<PagedListWrapper<MessageDto>>($"/api/messages?PageNumber={query.PageNumber}&PageSize={query.PageSize}&SenderId={currentUserId}&ReceiverId={query.ReceiverId}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Mesajlar yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<MessageDto>.Empty(query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Mesaj detayını göster
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<MessageDto>>($"/api/messages/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Mesaj detayları yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Yeni mesaj oluşturma sayfası
    /// </summary>
    public IActionResult Create(Guid? receiverId = null, Guid? propertyId = null, Guid? reservationId = null)
    {
        ViewBag.ReceiverId = receiverId;
        ViewBag.PropertyId = propertyId;
        ViewBag.ReservationId = reservationId;
        return View();
    }

    /// <summary>
    /// Mesaj düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<MessageDto>>($"/api/messages/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Mesaj bilgileri yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Kullanıcının mesajlarını göster
    /// </summary>
    public async Task<IActionResult> MyMessages([FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            // Session'dan UserId'yi al
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                return RedirectToAction("Login", "Users");
            }

            var response = await _httpClient.GetAsync<PagedListWrapper<MessageDto>>($"/api/messages?SenderId={parsedUserId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Mesajlarınız yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<MessageDto>.Empty(pageNumber, pageSize));
    }

    /// <summary>
    /// Gelen mesajları göster
    /// </summary>
    public async Task<IActionResult> Inbox([FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            // Session'dan UserId'yi al
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                return RedirectToAction("Login", "Users");
            }

            var response = await _httpClient.GetAsync<PagedListWrapper<MessageDto>>($"/api/messages?ReceiverId={parsedUserId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Gelen mesajlar yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<MessageDto>.Empty(pageNumber, pageSize));
    }
} 
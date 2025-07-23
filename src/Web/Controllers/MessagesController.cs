using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Messages.Queries.GetMessages;
using MinimalAirbnb.Application.Messages.Queries.GetMessageById;
using MinimalAirbnb.Application.Messages.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Messages Web Controller
/// </summary>
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
            var response = await _httpClient.GetAsync<PagedList<MessageDto>>($"/api/messages?PageNumber={query.PageNumber}&PageSize={query.PageSize}&SenderId={query.SenderId}&ReceiverId={query.ReceiverId}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Mesajlar yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<MessageDto>(new List<MessageDto>(), 0, query.PageNumber, query.PageSize));
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
    public async Task<IActionResult> MyMessages(Guid userId, [FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<MessageDto>>($"/api/messages?SenderId={userId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Mesajlarınız yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<MessageDto>(new List<MessageDto>(), 0, pageNumber, pageSize));
    }

    /// <summary>
    /// Gelen mesajları göster
    /// </summary>
    public async Task<IActionResult> Inbox(Guid userId, [FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<MessageDto>>($"/api/messages?ReceiverId={userId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Gelen mesajlar yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<MessageDto>(new List<MessageDto>(), 0, pageNumber, pageSize));
    }
} 
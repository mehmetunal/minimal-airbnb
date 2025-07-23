using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Users.Queries.GetUsers;
using MinimalAirbnb.Application.Users.Queries.GetUserById;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Users Web Controller
/// </summary>
public class UsersController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public UsersController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Kullanıcı profilini göster
    /// </summary>
    public async Task<IActionResult> Profile(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<UserDto>>($"/api/users/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Kullanıcı profili yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Kullanıcı kayıt sayfası
    /// </summary>
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Kullanıcı giriş sayfası
    /// </summary>
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Kullanıcı ayarları sayfası
    /// </summary>
    public async Task<IActionResult> Settings(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<UserDto>>($"/api/users/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Kullanıcı ayarları yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }
} 
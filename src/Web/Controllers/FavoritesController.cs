using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MinimalAirbnb.Application.Favorites.Queries.GetFavorites;
using MinimalAirbnb.Application.Favorites.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Web.Models;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Favorites Web Controller
/// </summary>
[Authorize]
public class FavoritesController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;

    public FavoritesController(IMaggsoftHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Favori listesini göster
    /// </summary>
    public async Task<IActionResult> Index([FromQuery] GetFavoritesQuery query)
    {
        try
        {
            // Current user'dan UserId'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return RedirectToAction("Login", "Users");
            }

            var response = await _httpClient.GetAsync<PagedListWrapper<FavoriteDto>>($"/api/favorites?PageNumber={query.PageNumber}&PageSize={query.PageSize}&UserId={userId}");

            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Favoriler yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<FavoriteDto>.Empty(query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Kullanıcının favorilerini göster
    /// </summary>
    public async Task<IActionResult> MyFavorites([FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            // Current user'dan UserId'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return RedirectToAction("Login", "Users");
            }

            var response = await _httpClient.GetAsync<PagedListWrapper<FavoriteDto>>($"/api/favorites?UserId={userId}&PageNumber={pageNumber}&PageSize={pageSize}");

            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Favorileriniz yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<FavoriteDto>.Empty(pageNumber, pageSize));
    }
}

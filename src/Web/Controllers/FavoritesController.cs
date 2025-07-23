using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Favorites.Queries.GetFavorites;
using MinimalAirbnb.Application.Favorites.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Favorites Web Controller
/// </summary>
public class FavoritesController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public FavoritesController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Favori listesini göster
    /// </summary>
    public async Task<IActionResult> Index([FromQuery] GetFavoritesQuery query)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<FavoriteDto>>($"/api/favorites?PageNumber={query.PageNumber}&PageSize={query.PageSize}&UserId={query.UserId}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Favoriler yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<FavoriteDto>(new List<FavoriteDto>(), 0, query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Kullanıcının favorilerini göster
    /// </summary>
    public async Task<IActionResult> MyFavorites(Guid userId, [FromQuery] int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedList<FavoriteDto>>($"/api/favorites?UserId={userId}&PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch
        {
            ModelState.AddModelError("", "Favorileriniz yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<FavoriteDto>(new List<FavoriteDto>(), 0, pageNumber, pageSize));
    }
} 
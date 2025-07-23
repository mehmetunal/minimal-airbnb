using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Properties.Queries.GetProperties;
using MinimalAirbnb.Application.Properties.Queries.GetPropertyById;
using MinimalAirbnb.Application.Properties.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Web.Models;

namespace MinimalAirbnb.Web.Controllers;

/// <summary>
/// Properties Web Controller
/// </summary>
public class PropertiesController : Controller
{
    private readonly IMaggsoftHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PropertiesController(IMaggsoftHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Properties listesini göster
    /// </summary>
    public async Task<IActionResult> Index([FromQuery] GetPropertiesQuery query)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedListWrapper<PropertyDto>>($"/api/properties?PageNumber={query.PageNumber}&PageSize={query.PageSize}&City={query.City}&MinPrice={query.MinPrice}&MaxPrice={query.MaxPrice}&PropertyType={query.PropertyType}");

            if (response is { Data: not null })
            {
                return View(response);
            }
        }
        catch
        {
            // Log error
            ModelState.AddModelError("", "Properties yüklenirken bir hata oluştu.");
        }

        return View(PagedListWrapper<PropertyDto>.Empty(query.PageNumber, query.PageSize));
    }

    /// <summary>
    /// Property detayını göster
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<PropertyDto>>($"/api/properties/{id}");

            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch
        {
            // Log error
            ModelState.AddModelError("", "Property detayları yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Property arama sayfası
    /// </summary>
    public IActionResult Search()
    {
        return View();
    }
}

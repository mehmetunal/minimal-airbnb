using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Properties.Commands.CreateProperty;
using MinimalAirbnb.Application.Properties.Commands.UpdateProperty;
using MinimalAirbnb.Application.Properties.Commands.DeleteProperty;
using MinimalAirbnb.Application.Properties.Queries.GetProperties;
using MinimalAirbnb.Application.Properties.Queries.GetPropertyById;
using MinimalAirbnb.Application.Properties.DTOs;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Properties Controller
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
            var response = await _httpClient.GetAsync<PagedList<PropertyDto>>($"/api/properties?PageNumber={query.PageNumber}&PageSize={query.PageSize}&City={query.City}&MinPrice={query.MinPrice}&MaxPrice={query.MaxPrice}&PropertyType={query.PropertyType}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch (Exception ex)
        {
            // Log error
            ModelState.AddModelError("", "Properties yüklenirken bir hata oluştu.");
        }

        return View(new PagedList<PropertyDto>(new List<PropertyDto>(), 0, query.PageNumber, query.PageSize));
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
        catch (Exception ex)
        {
            // Log error
            ModelState.AddModelError("", "Property detayları yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Property oluşturma sayfası
    /// </summary>
    public IActionResult Create()
    {
        return View(new CreatePropertyCommand());
    }

    /// <summary>
    /// Property oluştur
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreatePropertyCommand command)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await _httpClient.PostAsync<object>("/api/properties", command);
                
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Property başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log error
                ModelState.AddModelError("", "Property oluşturulurken bir hata oluştu.");
            }
        }

        return View(command);
    }

    /// <summary>
    /// Property düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<PropertyDto>>($"/api/properties/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                var command = new UpdatePropertyCommand
                {
                    Id = response.Data.Id,
                    Title = response.Data.Title,
                    Description = response.Data.Description,
                    PropertyType = response.Data.PropertyType.ToString(),
                    PricePerNight = response.Data.PricePerNight,
                    Address = response.Data.Address,
                    City = response.Data.City,
                    Country = response.Data.Country,
                    PostalCode = response.Data.PostalCode ?? string.Empty,
                    Latitude = (decimal)response.Data.Latitude,
                    Longitude = (decimal)response.Data.Longitude,
                    Bedrooms = response.Data.BedroomCount,
                    Bathrooms = response.Data.BathroomCount,
                    MaxGuests = response.Data.MaxGuestCount,
                    MinimumStay = response.Data.MinimumStayDays,
                    MaximumStay = response.Data.MaximumStayDays
                };
                return View(command);
            }
        }
        catch (Exception ex)
        {
            // Log error
            ModelState.AddModelError("", "Property bilgileri yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Property güncelle
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(UpdatePropertyCommand command)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await _httpClient.PutAsync<object>($"/api/properties/{command.Id}", command);
                
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Property başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log error
                ModelState.AddModelError("", "Property güncellenirken bir hata oluştu.");
            }
        }

        return View(command);
    }

    /// <summary>
    /// Property silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<PropertyDto>>($"/api/properties/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }
        }
        catch (Exception ex)
        {
            // Log error
            ModelState.AddModelError("", "Property bilgileri yüklenirken bir hata oluştu.");
        }

        return NotFound();
    }

    /// <summary>
    /// Property sil
    /// </summary>
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync("/api/properties", id);
            
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Property başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            // Log error
            ModelState.AddModelError("", "Property silinirken bir hata oluştu.");
        }

        return RedirectToAction(nameof(Index));
    }
} 
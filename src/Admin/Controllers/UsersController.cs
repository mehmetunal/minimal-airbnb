using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinimalAirbnb.Application.Users.Queries.GetUsers;
using MinimalAirbnb.Application.Users.Queries.GetUserById;
using MinimalAirbnb.Application.Users.Commands.UpdateUser;
using MinimalAirbnb.Application.Users.Commands.DeleteUser;
using Maggsoft.Framework.HttpClientApi;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Base;
using MinimalAirbnb.Admin.Models;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Users Controller
/// </summary>
[Authorize(Roles = "Admin")]
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
    /// Kullanıcıları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync<PagedListWrapper<MinimalAirbnb.Application.Users.DTOs.UserDto>>($"/api/users?PageNumber={pageNumber}&PageSize={pageSize}");
            
            if (response != null)
            {
                return View(response);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Kullanıcılar yüklenirken bir hata oluştu.";
        }

        return View(PagedListWrapper<MinimalAirbnb.Application.Users.DTOs.UserDto>.Empty(pageNumber, pageSize));
    }

    /// <summary>
    /// Kullanıcı detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<MinimalAirbnb.Application.Users.DTOs.UserDto>>($"/api/users/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }

            TempData["Error"] = "Kullanıcı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Kullanıcı detayları yüklenirken bir hata oluştu.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Kullanıcı düzenleme sayfası
    /// </summary>
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<MinimalAirbnb.Application.Users.DTOs.UserDto>>($"/api/users/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }

            TempData["Error"] = "Kullanıcı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Kullanıcı bilgileri yüklenirken bir hata oluştu.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Kullanıcı düzenleme işlemi
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var response = await _httpClient.PutAsync<object>($"/api/users/{id}", command);
                
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Kullanıcı başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Kullanıcı güncellenirken bir hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı güncellenirken bir hata oluştu.";
            }
        }

        // Hata durumunda kullanıcı bilgilerini tekrar yükle
        try
        {
            var userResponse = await _httpClient.GetAsync<Result<MinimalAirbnb.Application.Users.DTOs.UserDto>>($"/api/users/{id}");
            if (userResponse != null && userResponse.IsSuccess && userResponse.Data != null)
            {
                // Command'deki değerleri UserDto'ya kopyala
                userResponse.Data.FirstName = command.FirstName;
                userResponse.Data.LastName = command.LastName;
                userResponse.Data.Email = command.Email;
                userResponse.Data.PhoneNumber = command.PhoneNumber;
                userResponse.Data.DateOfBirth = command.DateOfBirth;
                userResponse.Data.UserType = command.UserType;
                userResponse.Data.Gender = command.Gender;
                userResponse.Data.EmailConfirmed = command.EmailConfirmed;
                
                return View(userResponse.Data);
            }
        }
        catch
        {
            // Hata durumunda boş UserDto döndür
        }
        
        return View(new MinimalAirbnb.Application.Users.DTOs.UserDto());
    }

    /// <summary>
    /// Kullanıcı silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync<Result<MinimalAirbnb.Application.Users.DTOs.UserDto>>($"/api/users/{id}");
            
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return View(response.Data);
            }

            TempData["Error"] = "Kullanıcı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Kullanıcı bilgileri yüklenirken bir hata oluştu.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Kullanıcı silme işlemi
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/users/{id}", new { id });

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Kullanıcı başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = "Kullanıcı silinirken bir hata oluştu.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Kullanıcı silinirken bir hata oluştu.";
        }

        return RedirectToAction(nameof(Index));
    }
} 
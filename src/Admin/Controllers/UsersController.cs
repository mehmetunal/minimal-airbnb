using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Users.Queries.GetUsers;
using MinimalAirbnb.Application.Users.Queries.GetUserById;
using MinimalAirbnb.Application.Users.Commands.UpdateUser;
using MinimalAirbnb.Application.Users.Commands.DeleteUser;
using MediatR;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Admin.Controllers;

/// <summary>
/// Admin Users Controller
/// </summary>
public class UsersController : Controller
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Kullanıcıları listele
    /// </summary>
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var query = new GetUsersQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return View(result);
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Kullanıcılar yüklenirken bir hata oluştu.";
            return View(new PagedList<MinimalAirbnb.Application.Users.DTOs.UserDto>(new List<MinimalAirbnb.Application.Users.DTOs.UserDto>(), 0, pageSize, 0));
        }
    }

    /// <summary>
    /// Kullanıcı detayı
    /// </summary>
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                return View(result.Data);
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
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                return View(result.Data);
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
                var result = await _mediator.Send(command);
                
                if (result.IsSuccess)
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

        return View(command);
    }

    /// <summary>
    /// Kullanıcı silme sayfası
    /// </summary>
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                return View(result.Data);
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
            var command = new DeleteUserCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
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
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Common.Models;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Tüm API Controller'lar için base sınıf
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private ISender? _mediator;

    /// <summary>
    /// MediatR sender instance'ı
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    /// <summary>
    /// Başarılı response döndürür
    /// </summary>
    protected ActionResult<ApiResponse<T>> Success<T>(T data, string message = "İşlem başarılı")
    {
        return Ok(new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        });
    }

    /// <summary>
    /// Başarılı response döndürür (data olmadan)
    /// </summary>
    protected ActionResult<ApiResponse<object>> Success(string message = "İşlem başarılı")
    {
        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = message
        });
    }

    /// <summary>
    /// Hata response döndürür
    /// </summary>
    protected ActionResult<ApiResponse<object>> Error(string message, int statusCode = 400)
    {
        var response = new ApiResponse<object>
        {
            Success = false,
            Message = message
        };

        return statusCode switch
        {
            400 => BadRequest(response),
            401 => Unauthorized(response),
            403 => Forbid(),
            404 => NotFound(response),
            500 => StatusCode(500, response),
            _ => BadRequest(response)
        };
    }

    /// <summary>
    /// Validation hatası response döndürür
    /// </summary>
    protected ActionResult<ApiResponse<object>> ValidationError(string message, object? errors = null)
    {
        return BadRequest(new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Errors = errors
        });
    }
} 
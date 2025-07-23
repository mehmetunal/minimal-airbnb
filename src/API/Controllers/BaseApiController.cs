using MediatR;
using Microsoft.AspNetCore.Mvc;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

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
    protected ActionResult<Result<T>> Success<T>(T data, string message = "İşlem başarılı") where T : class
    {
        return Ok(Result<T>.Success(data, new SuccessMessage("200",message)));
    }

    /// <summary>
    /// Başarılı response döndürür (data olmadan)
    /// </summary>
    protected ActionResult<Result<object>> Success(string message = "İşlem başarılı")
    {
        return Ok(Result<object>.Success(null, new SuccessMessage("200",message)));
    }

    /// <summary>
    /// Hata response döndürür
    /// </summary>
    protected ActionResult<Result<object>> Error(string message, int statusCode = 400)
    {
        var response = Result<object>.Failure(new Error(statusCode.ToString(), message));

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
    protected ActionResult<Result<object>> ValidationError(string message, object? errors = null)
    {
        return BadRequest(Result<object>.Failure(new Error("500",message)));
    }
} 
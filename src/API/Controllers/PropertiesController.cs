using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Properties.Commands.CreateProperty;
using MinimalAirbnb.Application.Properties.Commands.UpdateProperty;
using MinimalAirbnb.Application.Properties.Commands.DeleteProperty;
using MinimalAirbnb.Application.Properties.Queries.GetProperties;
using MinimalAirbnb.Application.Properties.Queries.GetPropertyById;
using MinimalAirbnb.Application.Properties.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Properties API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropertiesController : BaseApiController
{
    private readonly IMediator _mediator;

    public PropertiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Properties listesini getir
    /// </summary>
    [HttpGet]
    public async Task<PagedList<PropertyDto>> GetProperties([FromQuery] GetPropertiesQuery query)
    {
        var result = await _mediator.Send(query);
        return result?.Data ?? new PagedList<PropertyDto>(new List<PropertyDto>(), 0, query.PageNumber, query.PageSize);
    }

    /// <summary>
    /// Property detayını getir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<PropertyDto>>> GetPropertyById(Guid id)
    {
        var query = new GetPropertyByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Yeni property oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<object>>> CreateProperty([FromBody] CreatePropertyCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetPropertyById), new { id = result.Data }, result);
    }

    /// <summary>
    /// Property güncelle
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<Result<object>>> UpdateProperty(Guid id, [FromBody] UpdatePropertyCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Property sil
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<object>>> DeleteProperty(Guid id)
    {
        var command = new DeletePropertyCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}

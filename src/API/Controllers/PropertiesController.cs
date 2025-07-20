using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Application.Properties.Commands.CreateProperty;
using MinimalAirbnb.Application.Properties.Commands.UpdateProperty;
using MinimalAirbnb.Application.Properties.Commands.DeleteProperty;
using MinimalAirbnb.Application.Properties.Queries.GetProperties;
using MinimalAirbnb.Application.Properties.Queries.GetPropertyById;
using MinimalAirbnb.Application.Properties.DTOs;

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
    public async Task<ActionResult<PaginatedApiResponse<PropertyDto>>> GetProperties([FromQuery] GetPropertiesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Property detayını getir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PropertyDto>>> GetPropertyById(Guid id)
    {
        var query = new GetPropertyByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (!result.Success)
            return BadRequest(result);
            
        return Ok(result);
    }

    /// <summary>
    /// Yeni property oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<Guid>>> CreateProperty([FromBody] CreatePropertyCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.Success)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(GetPropertyById), new { id = result.Data }, result);
    }

    /// <summary>
    /// Property güncelle
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateProperty(Guid id, [FromBody] UpdatePropertyCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        
        if (!result.Success)
            return BadRequest(result);
            
        return Ok(result);
    }

    /// <summary>
    /// Property sil
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteProperty(Guid id)
    {
        var command = new DeletePropertyCommand { Id = id };
        var result = await _mediator.Send(command);
        
        if (!result.Success)
            return BadRequest(result);
            
        return Ok(result);
    }
} 
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Favorites.Commands.CreateFavorite;
using MinimalAirbnb.Application.Favorites.Commands.DeleteFavorite;
using MinimalAirbnb.Application.Favorites.Queries.GetFavorites;
using MinimalAirbnb.Application.Favorites.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Favorites API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FavoritesController : BaseApiController
{
    private readonly IMediator _mediator;

    public FavoritesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Favorites listesini getir
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<FavoriteDto>>> GetFavorites([FromQuery] GetFavoritesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Yeni favorite oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<object>>> CreateFavorite([FromBody] CreateFavoriteCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(GetFavorites), result);
    }

    /// <summary>
    /// Favorite sil
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<object>>> DeleteFavorite(Guid id)
    {
        var command = new DeleteFavoriteCommand { Id = id };
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return Ok(result);
    }
} 
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Users.Commands.CreateUser;
using MinimalAirbnb.Application.Users.Queries.GetUsers;
using MinimalAirbnb.Application.Users.Queries.GetUserById;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Users API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseApiController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Users listesini getir
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// User detayını getir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<UserDto>>> GetUserById(Guid id)
    {
        var query = new GetUserByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return Ok(result);
    }

    /// <summary>
    /// Yeni user oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<object>>> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(GetUserById), new { id = result.Data }, result);
    }
} 
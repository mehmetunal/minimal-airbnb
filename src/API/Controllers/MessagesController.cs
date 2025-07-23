using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Messages.Commands.CreateMessage;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Messages API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MessagesController : BaseApiController
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Yeni message oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<object>>> CreateMessage([FromBody] CreateMessageCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(CreateMessage), result);
    }
} 
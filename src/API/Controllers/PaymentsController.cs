using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Payments.Commands.CreatePayment;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Payments API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : BaseApiController
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Yeni payment oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<object>>> CreatePayment([FromBody] CreatePaymentCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(CreatePayment), result);
    }
} 
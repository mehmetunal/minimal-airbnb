using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Reviews.Commands.CreateReview;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Reviews API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReviewsController : BaseApiController
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Yeni review oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<object>>> CreateReview([FromBody] CreateReviewCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(CreateReview), result);
    }
} 
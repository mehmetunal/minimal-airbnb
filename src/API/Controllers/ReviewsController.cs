using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Commands.Review;
using MinimalAirbnb.Application.Reviews.Queries.GetReviews;
using MinimalAirbnb.Application.Reviews.Queries.GetReviewById;
using MinimalAirbnb.Application.Reviews.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

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
    /// Reviews listesini getir
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<ReviewDto>>> GetReviews([FromQuery] GetReviewsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// ID'ye göre review getir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ReviewDto>>> GetReviewById(Guid id)
    {
        var query = new GetReviewByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
            return NotFound(result);
            
        return Ok(result);
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
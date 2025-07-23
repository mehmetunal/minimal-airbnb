using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Reservations.Commands.CreateReservation;
using MinimalAirbnb.Application.Reservations.Queries.GetReservations;
using MinimalAirbnb.Application.Reservations.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.API.Controllers;

/// <summary>
/// Reservations API Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : BaseApiController
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Reservations listesini getir
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<ReservationDto>>> GetReservations([FromQuery] GetReservationsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Yeni reservation oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<object>>> CreateReservation([FromBody] CreateReservationCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(GetReservations), result);
    }
} 
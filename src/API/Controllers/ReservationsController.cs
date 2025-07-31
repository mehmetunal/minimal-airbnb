using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Commands.Reservation;
using MinimalAirbnb.Application.Reservations.Commands.DeleteReservation;

using MinimalAirbnb.Application.Reservations.Queries.GetReservations;
using MinimalAirbnb.Application.Reservations.Queries.GetReservationById;
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
    /// ID'ye göre reservation getir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ReservationDto>>> GetReservationById(Guid id)
    {
        var query = new GetReservationByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
            return NotFound(result);
            
        return Ok(result);
    }

    /// <summary>
    /// Yeni reservation oluştur
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Result<CreateReservationResponseDto>>> CreateReservation([FromBody] CreateReservationCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return Ok(result);
    }

    /// <summary>
    /// Rezervasyon güncelle
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<Result<object>>> UpdateReservation(Guid id, [FromBody] MinimalAirbnb.Application.Reservations.Commands.UpdateReservation.UpdateReservationCommand command)
    {
        command.ReservationId = id;
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return Ok(result);
    }

    /// <summary>
    /// Rezervasyon sil
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<object>>> DeleteReservation(Guid id)
    {
        var command = new MinimalAirbnb.Application.Reservations.Commands.DeleteReservation.DeleteReservationCommand { ReservationId = id };
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return Ok(result);
    }
} 
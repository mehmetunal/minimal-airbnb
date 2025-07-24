using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAirbnb.Application.Payments.Commands.CreatePayment;
using MinimalAirbnb.Application.Payments.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

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
    public async Task<ActionResult<Result<CreatePaymentResponseDto>>> CreatePayment([FromBody] CreatePaymentCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
            
        return CreatedAtAction(nameof(CreatePayment), result);
    }

    /// <summary>
    /// Ödeme durumunu kontrol et
    /// </summary>
    [HttpGet("status/{transactionId}")]
    public async Task<ActionResult<Result<string>>> CheckPaymentStatus(string transactionId)
    {
        // TODO: Implement payment status check
        return Ok(Result<string>.Success("Success", new SuccessMessage("200", "Ödeme durumu başarıyla kontrol edildi.")));
    }

    /// <summary>
    /// Ödeme iadesi yap
    /// </summary>
    [HttpPost("refund/{paymentId}")]
    public async Task<ActionResult<Result<CreatePaymentResponseDto>>> RefundPayment(Guid paymentId, [FromBody] RefundRequest request)
    {
        // TODO: Implement payment refund
        return Ok(Result<CreatePaymentResponseDto>.Success(new CreatePaymentResponseDto(), new SuccessMessage("200", "İade işlemi başarıyla tamamlandı.")));
    }

    /// <summary>
    /// Ödeme iptal et
    /// </summary>
    [HttpPost("cancel/{paymentId}")]
    public async Task<ActionResult<Result<CreatePaymentResponseDto>>> CancelPayment(Guid paymentId, [FromBody] CancelRequest request)
    {
        // TODO: Implement payment cancellation
        return Ok(Result<CreatePaymentResponseDto>.Success(new CreatePaymentResponseDto(), new SuccessMessage("200", "Ödeme iptali başarıyla tamamlandı.")));
    }
}

/// <summary>
/// İade isteği
/// </summary>
public class RefundRequest
{
    public decimal Amount { get; set; }
    public string Reason { get; set; } = string.Empty;
}

/// <summary>
/// İptal isteği
/// </summary>
public class CancelRequest
{
    public string Reason { get; set; } = string.Empty;
} 
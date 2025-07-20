using MediatR;
using MinimalAirbnb.Application.DTOs.Message;

namespace MinimalAirbnb.Application.Commands.Message;

/// <summary>
/// Mesaj Güncelleme Komutu
/// </summary>
public class UpdateMessageCommand : IRequest<MessageResultDto>
{
    /// <summary>
    /// Mesaj güncelleme DTO
    /// </summary>
    public UpdateMessageDto UpdateMessageDto { get; set; } = new();
} 
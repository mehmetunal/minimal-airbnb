using MediatR;
using MinimalAirbnb.Application.DTOs.Message;

namespace MinimalAirbnb.Application.Commands.Message;

/// <summary>
/// Mesaj Oluşturma Komutu
/// </summary>
public class CreateMessageCommand : IRequest<MessageResultDto>
{
    /// <summary>
    /// Mesaj ekleme DTO
    /// </summary>
    public AddMessageDto AddMessageDto { get; set; } = new();
} 
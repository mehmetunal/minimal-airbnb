using MediatR;
using MinimalAirbnb.Application.DTOs.Message;

namespace MinimalAirbnb.Application.Queries.Message;

/// <summary>
/// ID'ye Göre Mesaj Getirme Sorgusu
/// </summary>
public class GetMessageByIdQuery : IRequest<MessageResultDto?>
{
    /// <summary>
    /// Mesaj ID
    /// </summary>
    public Guid Id { get; set; }
} 
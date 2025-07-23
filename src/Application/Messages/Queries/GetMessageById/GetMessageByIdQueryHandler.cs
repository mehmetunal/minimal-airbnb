using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Messages.Queries.GetMessageById;
using MinimalAirbnb.Application.Messages.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Messages.Queries.GetMessageById;

/// <summary>
/// Message by ID query handler'ı
/// </summary>
public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, Result<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessageByIdQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<MessageDto>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var message = await _messageRepository.GetByIdAsync(request.Id);

            if (message == null)
            {
                return Result<MessageDto>.Failure(new Error("404", "Belirtilen ID'ye sahip mesaj sistemde mevcut değil."));
            }

            var messageDto = new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Subject = message.Subject,
                Content = message.Content,
                ReservationId = message.ReservationId,
                PropertyId = message.PropertyId,
                IsRead = message.IsRead,
                ReadDate = message.ReadDate,
                ReplyToMessageId = message.ReplyToMessageId,
                MessageType = message.MessageType,
                Priority = message.Priority,
                Category = message.Category,
                AttachmentUrl = message.AttachmentUrl,
                CreatedAt = message.CreatedDate,
                UpdatedAt = message.ModifiedDate
            };

            return Result<MessageDto>.Success(messageDto, new SuccessMessage("200", "Mesaj bilgileri başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<MessageDto>.Failure(new Error("500", $"Mesaj getirilirken hata oluştu: {ex.Message}"));
        }
    }
}

using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Messages.Commands.CreateMessage;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Messages.Commands.CreateMessage;

/// <summary>
/// Mesaj oluşturma command handler'ı
/// </summary>
public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<object>>
{
    private readonly IMessageRepository _messageRepository;

    public CreateMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<object>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var message = new Message
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Content = request.Content,
                Subject = "Yeni Mesaj",
                IsRead = false
            };

            var createdMessage = await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();

            return Result<object>.Success(createdMessage.Id, new SuccessMessage("200", "Mesaj sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Mesaj oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
}

using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Interfaces;

namespace MinimalAirbnb.Application.Messages.Commands.UpdateMessage;

/// <summary>
/// Mesaj güncelleme handler'ı
/// </summary>
public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Result<object>>
{
    private readonly IMessageRepository _messageRepository;

    public UpdateMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<object>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var message = await _messageRepository.GetByIdAsync(request.Id);
            if (message == null)
            {
                return Result<object>.Failure(new Maggsoft.Core.Model.Error("MESSAGE_NOT_FOUND", "Mesaj bulunamadı."));
            }

            // Güncelleme işlemleri
            if (!string.IsNullOrEmpty(request.Subject))
                message.Subject = request.Subject;
            
            if (!string.IsNullOrEmpty(request.Content))
                message.Content = request.Content;
            
            if (request.IsRead.HasValue)
                message.IsRead = request.IsRead.Value;
            
            if (request.IsArchived.HasValue)
                message.IsArchived = request.IsArchived.Value;

            await _messageRepository.UpdateAsync(message);
            await _messageRepository.SaveChangesAsync();

            return Result<object>.Success("Mesaj başarıyla güncellendi.");
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Maggsoft.Core.Model.Error("MESSAGE_UPDATE_ERROR", $"Mesaj güncellenirken bir hata oluştu: {ex.Message}"));
        }
    }
} 
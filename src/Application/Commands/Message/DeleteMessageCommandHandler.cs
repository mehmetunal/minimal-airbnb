using MediatR;
using MinimalAirbnb.Application.Interfaces;

namespace MinimalAirbnb.Application.Commands.Message;

/// <summary>
/// Mesaj Silme Handler'ı
/// </summary>
public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
{
    private readonly IMessageRepository _messageRepository;

    public DeleteMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Mesajı getir
            var message = await _messageRepository.GetByIdAsync(request.Id);
            if (message == null)
            {
                throw new Exception("Mesaj bulunamadı.");
            }

            // Mesajı sil
            await _messageRepository.DeleteAsync(request.Id);
            await _messageRepository.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Mesaj silinirken hata oluştu: {ex.Message}");
        }
    }
} 
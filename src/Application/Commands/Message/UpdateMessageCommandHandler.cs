using MediatR;
using MinimalAirbnb.Application.DTOs.Message;
using MinimalAirbnb.Application.Interfaces;
using AutoMapper;

namespace MinimalAirbnb.Application.Commands.Message;

/// <summary>
/// Mesaj Güncelleme Handler'ı
/// </summary>
public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, MessageResultDto>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public UpdateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<MessageResultDto> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Mesajı getir
            var message = await _messageRepository.GetByIdAsync(request.UpdateMessageDto.Id);
            if (message == null)
            {
                throw new Exception("Mesaj bulunamadı.");
            }

            // Mesaj bilgilerini güncelle
            message.Subject = request.UpdateMessageDto.Subject;
            message.Content = request.UpdateMessageDto.Content;
            message.IsRead = request.UpdateMessageDto.IsRead;
            message.ModifiedDate = DateTime.UtcNow;

            // Değişiklikleri kaydet
            await _messageRepository.SaveChangesAsync();

            // Entity'den DTO'ya dönüştür
            var messageResultDto = _mapper.Map<MessageResultDto>(message);

            return messageResultDto;
        }
        catch (Exception ex)
        {
            throw new Exception($"Mesaj güncellenirken hata oluştu: {ex.Message}");
        }
    }
} 
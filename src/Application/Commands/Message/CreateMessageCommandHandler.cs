using MediatR;
using MinimalAirbnb.Application.DTOs.Message;
using MinimalAirbnb.Application.Interfaces;
using AutoMapper;

namespace MinimalAirbnb.Application.Commands.Message;

/// <summary>
/// Mesaj Oluşturma Handler'ı
/// </summary>
public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageResultDto>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public CreateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<MessageResultDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // DTO'dan entity'ye dönüştür
            var message = _mapper.Map<Domain.Entities.Message>(request.AddMessageDto);

            // Mesajı ekle
            await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();

            // Entity'den DTO'ya dönüştür
            var messageResultDto = _mapper.Map<MessageResultDto>(message);

            return messageResultDto;
        }
        catch (Exception ex)
        {
            throw new Exception($"Mesaj oluşturulurken hata oluştu: {ex.Message}");
        }
    }
} 
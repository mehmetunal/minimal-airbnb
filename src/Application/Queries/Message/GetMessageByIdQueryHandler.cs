using MediatR;
using MinimalAirbnb.Application.DTOs.Message;
using MinimalAirbnb.Application.Interfaces;
using AutoMapper;

namespace MinimalAirbnb.Application.Queries.Message;

/// <summary>
/// ID'ye Göre Mesaj Getirme Handler'ı
/// </summary>
public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageResultDto?>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public GetMessageByIdQueryHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<MessageResultDto?> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Mesajı getir
            var message = await _messageRepository.GetByIdAsync(request.Id);
            if (message == null)
            {
                return null;
            }

            // DTO'ya dönüştür
            var messageResultDto = _mapper.Map<MessageResultDto>(message);

            return messageResultDto;
        }
        catch (Exception ex)
        {
            throw new Exception($"Mesaj getirilirken hata oluştu: {ex.Message}");
        }
    }
} 
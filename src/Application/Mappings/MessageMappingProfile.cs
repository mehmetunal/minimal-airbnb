using AutoMapper;
using MinimalAirbnb.Application.DTOs.Message;
using MinimalAirbnb.Application.Messages.DTOs;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Application.Mappings;

/// <summary>
/// Message Mapping Profile
/// </summary>
public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        // Message -> MessageDto
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => 
                src.Sender != null ? $"{src.Sender.FirstName} {src.Sender.LastName}" : string.Empty))
            .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => 
                src.Sender != null ? (src.Sender.ProfilePictureUrl ?? src.Sender.ProfilePicture) : null))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => 
                src.Receiver != null ? $"{src.Receiver.FirstName} {src.Receiver.LastName}" : string.Empty))
            .ForMember(dest => dest.ReceiverPhotoUrl, opt => opt.MapFrom(src => 
                src.Receiver != null ? (src.Receiver.ProfilePictureUrl ?? src.Receiver.ProfilePicture) : null))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.Title : null))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.ModifiedDate));

        // Message -> MessageListDto
        CreateMap<Message, MessageListDto>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => 
                src.Sender != null ? $"{src.Sender.FirstName} {src.Sender.LastName}" : string.Empty))
            .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => 
                src.Sender != null ? (src.Sender.ProfilePictureUrl ?? src.Sender.ProfilePicture) : null))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => 
                src.Receiver != null ? $"{src.Receiver.FirstName} {src.Receiver.LastName}" : string.Empty))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.Title : null))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate));

        // Message -> MessageResultDto
        CreateMap<Message, MessageResultDto>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => 
                src.Sender != null ? $"{src.Sender.FirstName} {src.Sender.LastName}" : string.Empty))
            .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => 
                src.Sender != null ? (src.Sender.ProfilePictureUrl ?? src.Sender.ProfilePicture) : null))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => 
                src.Receiver != null ? $"{src.Receiver.FirstName} {src.Receiver.LastName}" : string.Empty))
            .ForMember(dest => dest.ReceiverPhotoUrl, opt => opt.MapFrom(src => 
                src.Receiver != null ? (src.Receiver.ProfilePictureUrl ?? src.Receiver.ProfilePicture) : null))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => 
                src.Property != null ? src.Property.Title : null))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.ModifiedDate));

        // AddMessageDto -> Message
        CreateMap<AddMessageDto, Message>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
} 
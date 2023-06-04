using AutoMapper;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;

namespace FavoriteLiterature.Moderation.Application.Mapping;

public sealed class AttachmentProfile : Profile
{
    public AttachmentProfile()
    {
        CreateMap<Attachment, GetAllAttachmentsItemResponse>();
    }
}
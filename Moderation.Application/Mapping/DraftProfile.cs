using AutoMapper;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using FavoriteLiterature.Moderation.Domain.RabbitMq;

namespace FavoriteLiterature.Moderation.Application.Mapping;

public sealed class DraftProfile : Profile
{
    public DraftProfile()
    {
        CreateMap<Draft, GetDraftResponse>();
        CreateMap<Draft, GetAllDraftsItemResponse>();
        CreateMap<Draft, AcceptedDraftMessage>();

        CreateMap<CreateDraftCommand, Draft>();
        CreateMap<UpdateDraftCommand, Draft>();
    }
}
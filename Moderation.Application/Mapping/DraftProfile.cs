using AutoMapper;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;

namespace FavoriteLiterature.Moderation.Application.Mapping;

public sealed class DraftProfile : Profile
{
    public DraftProfile()
    {
        CreateMap<Draft, GetAllDraftsItemResponse>();
    }
}
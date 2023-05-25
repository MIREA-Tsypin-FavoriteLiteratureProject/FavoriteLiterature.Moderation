using FavoriteLiterature.Moderation.Application.Handlers.Drafts.Commands;
using FavoriteLiterature.Moderation.Application.Handlers.Drafts.Queries;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using MediatR;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class MediatrExtensions
{
    public static void AddMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(_ => _.RegisterServicesFromAssemblies(typeof(Program).Assembly));

        #region Draft
        
        builder.Services.AddTransient<IRequestHandler<GetDraftQuery, GetDraftResponse>, GetDraftQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetAllDraftsQuery, GetAllDraftsResponse>, GetAllDraftsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateDraftCommand, CreateDraftResponse>, CreateDraftCommandHandler>();
        
        #endregion
    }
}
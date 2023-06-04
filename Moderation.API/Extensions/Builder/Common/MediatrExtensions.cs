using FavoriteLiterature.Moderation.Application.Handlers.Attachments.Commands;
using FavoriteLiterature.Moderation.Application.Handlers.Attachments.Queries;
using FavoriteLiterature.Moderation.Application.Handlers.Drafts.Commands;
using FavoriteLiterature.Moderation.Application.Handlers.Drafts.Queries;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;
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

        #region Attachment

        builder.Services.AddTransient<IRequestHandler<GetAttachmentQuery, GetAttachmentResponse>, GetAttachmentQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateAttachmentCommand, CreateAttachmentResponse>, CreateAttachmentCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<DeleteAttachmentCommand, DeleteAttachmentResponse>, DeleteAttachmentCommandHandler>();

        #endregion
        
        #region Draft

        builder.Services.AddTransient<IRequestHandler<GetDraftQuery, GetDraftResponse>, GetDraftQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetAllDraftsQuery, GetAllDraftsResponse>, GetAllDraftsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateDraftCommand, CreateDraftResponse>, CreateDraftCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateDraftCommand, UpdateDraftResponse>, UpdateDraftCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<VerifyDraftCommand, VerifyDraftResponse>, VerifyDraftCommandHandler>();

        #endregion
    }
}
using System.Net.Mime;
using FavoriteLiterature.Moderation.Application.Options;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace FavoriteLiterature.Moderation.Application.Handlers.Attachments.Queries;

public sealed class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, GetAttachmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AttachmentStorageOptions _attachmentStorageOptions;

    public GetAttachmentQueryHandler(IUnitOfWork unitOfWork, IOptions<AttachmentStorageOptions> attachmentStorageOptions)
    {
        _unitOfWork = unitOfWork;
        _attachmentStorageOptions = attachmentStorageOptions.Value;
    }

    public async Task<GetAttachmentResponse> Handle(GetAttachmentQuery query, CancellationToken cancellationToken)
    {
        var attachmentData = await _unitOfWork.AttachmentsRepository.GetAsync(x =>
                x.Id == query.Id,
            cancellationToken);
        if (attachmentData == null)
        {
            throw new ArgumentException("Attachment is not exists!", nameof(query.Id));
        }

        var fileName = attachmentData.FileId + Path.GetExtension(attachmentData.AttachmentTypeId);
        var filePath = Path.Combine(_attachmentStorageOptions.RootDirectory, fileName);

        if (!File.Exists(filePath))
        {
            throw new ArgumentException("File is not found in storage", nameof(attachmentData.FileId));
        }

        var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
        var isContentTypeFound = new FileExtensionContentTypeProvider()
            .TryGetContentType(fileName, out var contentType);

        return new GetAttachmentResponse(bytes, 
            isContentTypeFound ? contentType! : MediaTypeNames.Application.Octet, 
            fileName);
    }
}
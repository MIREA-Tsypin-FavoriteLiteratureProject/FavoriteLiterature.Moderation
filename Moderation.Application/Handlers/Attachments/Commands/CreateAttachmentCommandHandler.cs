using FavoriteLiterature.Moderation.Application.Options;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using MediatR;
using Microsoft.Extensions.Options;

namespace FavoriteLiterature.Moderation.Application.Handlers.Attachments.Commands;

public sealed class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, CreateAttachmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AttachmentStorageOptions _attachmentStorageOptions;

    public CreateAttachmentCommandHandler(IUnitOfWork unitOfWork, IOptions<AttachmentStorageOptions> attachmentStorageOptions)
    {
        _unitOfWork = unitOfWork;
        _attachmentStorageOptions = attachmentStorageOptions.Value;
    }

    public async Task<CreateAttachmentResponse> Handle(CreateAttachmentCommand command, CancellationToken cancellationToken)
    {
        var draftExists = await _unitOfWork.DraftsRepository.ExistsAsync(x =>
                x.Id == command.DraftId,
            cancellationToken);
        if (!draftExists)
        {
            throw new ArgumentException("Draft is not exists!", nameof(command.DraftId));
        }

        var fileId = Guid.NewGuid();
        var attachmentType = Path.GetExtension(command.File.FileName);
        var fileName = string.Concat(fileId, attachmentType);
        var filePath = Path.Combine(_attachmentStorageOptions.RootDirectory, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await command.File.CopyToAsync(stream, cancellationToken);

        var attachment = new Attachment
        {
            DraftId = command.DraftId,
            FileId = fileId,
            AttachmentTypeId = attachmentType
        };

        try
        {
            await _unitOfWork.BeginTransactionAsync(new[]
            {
                () => _unitOfWork.AttachmentsRepository.Add(attachment)
            });
        }
        catch (Exception)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            throw;
        }

        return new CreateAttachmentResponse(attachment.Id);
    }
}
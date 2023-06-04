using FavoriteLiterature.Moderation.Application.Options;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using MediatR;
using Microsoft.Extensions.Options;

namespace FavoriteLiterature.Moderation.Application.Handlers.Attachments.Commands;

public sealed class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, DeleteAttachmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AttachmentStorageOptions _attachmentStorageOptions;

    public DeleteAttachmentCommandHandler(IUnitOfWork unitOfWork, IOptions<AttachmentStorageOptions> attachmentStorageOptions)
    {
        _unitOfWork = unitOfWork;
        _attachmentStorageOptions = attachmentStorageOptions.Value;
    }

    public async Task<DeleteAttachmentResponse> Handle(DeleteAttachmentCommand command, CancellationToken cancellationToken)
    {
        var attachmentData = await _unitOfWork.AttachmentsRepository.GetAsync(x =>
                x.Id == command.Id,
            cancellationToken);
        if (attachmentData == null)
        {
            throw new ArgumentException("Attachment is not found!", nameof(command.Id));
        }

        var fileName = attachmentData.FileId + Path.GetExtension(attachmentData.AttachmentTypeId);
        var filePath = Path.Combine(_attachmentStorageOptions.RootDirectory, fileName);

        if (File.Exists(filePath))
        { 
            File.Delete(filePath);
        }

        await _unitOfWork.BeginTransactionAsync(new[]
        {
            () => _unitOfWork.AttachmentsRepository.Remove(attachmentData)
        });

        return new DeleteAttachmentResponse(attachmentData.Id);
    }
}
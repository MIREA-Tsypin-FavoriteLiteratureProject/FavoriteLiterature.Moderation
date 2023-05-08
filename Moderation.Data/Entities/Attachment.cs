using FavoriteLiterature.Moderation.Data.Entities.Abstract;

namespace FavoriteLiterature.Moderation.Data.Entities;

/// <summary>
/// Вложение
/// </summary>
public sealed class Attachment : BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор черновика писателя
    /// </summary>
    public Guid DraftId { get; set; }
    public Draft Draft { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор файла,
    /// его номер на файл-сервере
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    /// Тип вложения
    /// </summary>
    public string AttachmentTypeId { get; set; } = null!;
}
using FavoriteLiterature.Moderation.Data.Entities.Abstract;

namespace FavoriteLiterature.Moderation.Data.Entities;

/// <summary>
/// Черновик
/// </summary>
public sealed class Draft : BaseEntity
{
    /// <summary>
    /// Наименование произведения
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Краткое описание произведения 
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ссылка на авторов произведения
    /// </summary>
    public List<Guid> Authors { get; set; }

    /// <summary>
    /// Ссылка на жанры произведения
    /// </summary>
    public List<Guid> Genres { get; set; }

    /// <summary>
    /// Ссылка на вложения
    /// </summary>
    public List<Attachment> Attachments { get; set; }
}
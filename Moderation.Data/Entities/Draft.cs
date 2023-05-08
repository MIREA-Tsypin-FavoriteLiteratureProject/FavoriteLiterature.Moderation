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
    /// Авторы произведения
    /// </summary>
    public ICollection<Guid> Authors { get; } = new List<Guid>();
    
    /// <summary>
    /// Жанры произведения
    /// </summary>
    public ICollection<Guid> Genres { get; } = new List<Guid>();
}
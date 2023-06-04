namespace FavoriteLiterature.Moderation.Data.Entities.Abstract;

/// <summary>
/// Базовая сущность,
/// имеющая общие поля у большинства таблиц
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; private set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}
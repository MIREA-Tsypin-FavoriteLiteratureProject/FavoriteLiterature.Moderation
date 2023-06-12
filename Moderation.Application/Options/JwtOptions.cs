namespace FavoriteLiterature.Moderation.Application.Options;

public class JwtOptions
{
    /// <value>
    /// Свойство: <see cref="Issuer"/>. Тип: <see langword="string"/>.
    /// Издатель токена.
    /// </value>
    public string Issuer { get; set; } = null!;

    /// <value>
    /// Свойство: <see cref="SecretKey"/>. Тип: <see langword="string"/>.
    /// Ключ безопасности.
    /// </value>
    public string SecretKey { get; set; } = null!;

    /// <value>
    /// Свойство: <see cref="LifetimeInMinutes"/>. Тип: <see langword="int"/>.
    /// Время жизни токена (в минутах).
    /// </value>
    public int LifetimeInMinutes { get; set; }
}
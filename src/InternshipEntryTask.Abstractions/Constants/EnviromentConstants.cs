namespace InternshipEntryTask.Abstractions.Constants;

/// <summary>
/// Константы переменных окружения
/// </summary>
public class EnviromentConstants
{
    /// <summary>
    /// Секция с настройками игры
    /// </summary>
    public const string GAME_SECTION_NAME = "GameSettings";
    
    /// <summary>
    /// Ключ для получаения ширины доски
    /// </summary>
    public const string GAME_WIDTH_NAME = "Width";

    /// <summary>
    /// Ключ для получаения высоты доски
    /// </summary>
    public const string GAME_HEIGHT_NAME = "Height";

    /// <summary>
    /// Ключ для получаения длины для выигрыша
    /// </summary>
    public const string GAME_WINLENGTH_NAME = "WinLength";

    /// <summary>
    /// Секция с настройками подключения
    /// </summary>
    public const string CONNECTION_STRING_SECTION = "ConnectionStrings";

    /// <summary>
    /// Ключ для получения строки подключения в БД
    /// </summary>
    public const string DEFAULT_CONNECTION_STRING = "PostgreSqlConnection";

    /// <summary>
    /// Секция с настройками MemmoryCache
    /// </summary>
    public const string CACHE_SECTION = "Cache";

    /// <summary>
    /// Время жизни внутри Memmory Cache
    /// </summary>
    public const string CACHE_TTL = "Ttl";
}

namespace InternshipEntryTask.Abstractions.Constants;

/// <summary>
/// Константы ключей заголовков запросов
/// </summary>
public class HeadersKeysConstants
{
    /// <summary>
    /// Ключ заголовка управляет отображением доски
    /// </summary>
    public const string SHOW_BOARD_KEY = "X-Show-Board";

    /// <summary>
    /// Ключ заголовка ключ доступа к созданию хода
    /// </summary>
    public const string ACCESS_KEY = "X-Access-Key";

    /// <summary>
    /// Ключ заголовка ключ присоединения к игре
    /// </summary>
    public const string JOIN_KEY = "X-Join-Key";
}

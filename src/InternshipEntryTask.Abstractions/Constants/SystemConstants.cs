namespace InternshipEntryTask.Abstractions.Constants;

/// <summary>
/// Системные константы
/// </summary>
public class SystemConstants
{
    /// <summary>
    /// Формат ключа ETag
    /// </summary>
    public const string ETAG_KEY_FORMAT = "etag:move:{0}";

    /// <summary>
    /// Формат значения ETag
    /// </summary>
    public const string ETAG_VALUE_FORMAT = "{0}:{1}";

    /// <summary>
    /// Ключ для идентификатора запроса в расширениях ProblemDetails
    /// </summary>
    public const string REQUEST_ID_PROBLEM_DETAIL = "requestId";
    
    /// <summary>
    /// Ключ для trace ID в расширениях ProblemDetails
    /// </summary>
    public const string TRACE_ID_PROBLEM_DETAIL = "traceId";

    /// <summary>
    /// Разделитель между HTTP-методом и путем запроса в ProblemDetails.Instance
    /// </summary>
    public const char HTTP_METHOD_SEPARATOR = ' ';
}

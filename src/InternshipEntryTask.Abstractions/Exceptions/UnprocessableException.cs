namespace InternshipEntryTask.Abstractions.Exceptions;

/// <summary>
/// Исключение сервер понял запрос но не может его обработать
/// </summary>
public class UnprocessableException : Exception
{

    /// <summary>
    /// Создает объект <inheritdoc cref="UnprocessableException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="innerException">Внутреннее исключение</param>
    public UnprocessableException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="UnprocessableException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    public UnprocessableException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="UnprocessableException"/>
    /// </summary>
    public UnprocessableException()
    {
    }
}
namespace InternshipEntryTask.Abstractions.Exceptions;

/// <summary>
/// Исключение о конфликте
/// </summary>
public class ConflictException : Exception
{

    /// <summary>
    /// Создает объект <inheritdoc cref="ConflictException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="innerException">Внутреннее исключение</param>
    public ConflictException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="ConflictException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    public ConflictException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="ConflictException"/>
    /// </summary>
    public ConflictException()
    {
    }
}
namespace InternshipEntryTask.Abstractions.Exceptions;

/// <summary>
/// Базовое исключение для игры
/// </summary>
public class BaseGameException : Exception
{
    /// <summary>
    /// Создает объект <inheritdoc cref="BaseGameException"/>
    /// </summary>
    /// <param name="message">Сообщение в ошибке</param>
    /// <param name="innerException">Внутреннее исключение</param>
    public BaseGameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="BaseGameException"/>
    /// </summary>
    /// <param name="message">Сообщение в ошибке</param>
    public BaseGameException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="BaseGameException"/>
    /// </summary>
    public BaseGameException()
    {
    }
}
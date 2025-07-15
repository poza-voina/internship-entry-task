namespace InternshipEntryTask.Abstractions.Exceptions;


/// <summary>
/// Исключение о отстуствии сущности
/// </summary>
public class EntityNotFoundException : Exception
{

    /// <summary>
    /// Создает объект <inheritdoc cref="EntityNotFoundException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="innerException">Внутреннее исключение</param>
    public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="EntityNotFoundException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    public EntityNotFoundException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="EntityNotFoundException"/>
    /// </summary>
    public EntityNotFoundException()
    {
    }
}

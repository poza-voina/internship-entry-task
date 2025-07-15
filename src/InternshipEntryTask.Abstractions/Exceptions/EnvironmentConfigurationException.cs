namespace InternshipEntryTask.Abstractions.Exceptions;


/// <summary>
/// Исключение конфигурации переменных окружения
/// </summary>
public class EnvironmentConfigurationException : Exception
{

    /// <summary>
    /// Создает объект <inheritdoc cref="EnvironmentConfigurationException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="innerException">Внутренняя ошибка</param>
    public EnvironmentConfigurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="EnvironmentConfigurationException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    public EnvironmentConfigurationException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="EnvironmentConfigurationException"/>
    /// </summary>
    public EnvironmentConfigurationException()
    {
    }
}
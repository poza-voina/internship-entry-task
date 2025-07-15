namespace InternshipEntryTask.Abstractions.Exceptions;


/// <summary>
/// Исключение конфигурации переменных окружения
/// </summary>
public class EnviromentConfigurationException : Exception
{

    /// <summary>
    /// Создает объект <inheritdoc cref="EnviromentConfigurationException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="innerException">Внутренняя ошибка</param>
    public EnviromentConfigurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="EnviromentConfigurationException"/>
    /// </summary>
    /// <param name="message">Сообщение</param>
    public EnviromentConfigurationException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Создает объект <inheritdoc cref="EnviromentConfigurationException"/>
    /// </summary>
    public EnviromentConfigurationException()
    {
    }
}
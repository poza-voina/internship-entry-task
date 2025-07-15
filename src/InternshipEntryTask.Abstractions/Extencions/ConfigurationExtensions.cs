using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Abstractions.Exceptions;
using Microsoft.Extensions.Configuration;

namespace InternshipEntryTask.Api.Extenctions;

/// <summary>
/// Класс расширений для конфигурации
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Получает значение из конфигурации и вызывает ошибку если значение отсутвует
    /// </summary>
    /// <typeparam name="T">Тип значение</typeparam>
    /// <param name="section">Секция</param>
    /// <param name="key">Ключ</param>
    /// <returns>Значение из конфигурации</returns>
    public static T GetRequiredValue<T>(this IConfigurationSection section, string key)
    {
        var value = section.GetValue<T>(key);

        if (value is null || (value is string s && string.IsNullOrWhiteSpace(s)))
        {
            throw new EnvironmentConfigurationException(string.Format(MessagesConstants.ENVIROMENT_ERROR_MESSAGE_FORMAT, section.Path, key));
        }

        return value;
    }
}

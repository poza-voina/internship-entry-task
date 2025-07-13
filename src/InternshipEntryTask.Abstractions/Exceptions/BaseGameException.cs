namespace InternshipEntryTask.Abstractions.Exceptions;

/// <summary>
/// Базовое исключение для игры
/// </summary>
public class BaseGameException : Exception
{
    public BaseGameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public BaseGameException(string? message) : base(message)
    {
    }

    public BaseGameException()
    {
    }
}
namespace InternshipEntryTask.Abstractions.Exceptions;

public class EnviromentConfigurationException : Exception
{
    public EnviromentConfigurationException()
    {
    }

    public EnviromentConfigurationException(string? message) : base(message)
    {
    }

    public EnviromentConfigurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
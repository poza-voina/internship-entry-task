namespace InternshipEntryTask.Api.IntegrationTests.Base;

public class CustomWebApplicationFactoryOptions
{
    public string? PathToEnvironment { get; set; }

    public string? ConnectionString { get; set; }

    public string? DatabaseSchemaName { get; set; }
}
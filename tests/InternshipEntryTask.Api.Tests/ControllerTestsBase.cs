using InternshipEntryTask.Api.IntegrationTests.Base;
using InternshipEntryTask.Api.Tests.Base;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace InternshipEntryTask.Api.Tests;

public abstract class ControllerTestsBase
{
    private const string SCHEMA_FORMAT = "test_{0}";

    public ControllerTestsBase()
    {
    }

    public JsonSerializerOptions DefaultSerializerOptions { get; } = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public HttpClient CreateIsolatedClient(IsolatedClientOptions isolatedClientOptions)
    {
        return new CustomWebApplicationFactory(
            options =>
            {
                if (isolatedClientOptions.ContainerFixture is { })
                {
                    var schemaName = NewSchemaName;
                    options.ConnectionString = $"{isolatedClientOptions.ContainerFixture.ConnectionString};Search Path={schemaName}";
                    options.DatabaseSchemaName = schemaName;
                }
                if (isolatedClientOptions.PathToEnvironment is { })
                {
                    options.PathToEnvironment = isolatedClientOptions.PathToEnvironment;
                }
            })
            .CreateClient();
    }

    public StringContent EmptyContent { get; } =
        new StringContent(string.Empty, Encoding.UTF8, "application/json");

    private string NewSchemaName =>
        string.Format(SCHEMA_FORMAT, Guid.NewGuid());

}

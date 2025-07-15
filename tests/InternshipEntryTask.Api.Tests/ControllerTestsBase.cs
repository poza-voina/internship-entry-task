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
    protected readonly TestOptions TestOptions;

    public ControllerTestsBase(TestOptions options)
    {
        TestOptions = options;
    }

    public JsonSerializerOptions DefaultSerializerOptions { get; } = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public HttpClient CreateIsolatedClient()
    {
        return new CustomWebApplicationFactory(
            options =>
            {
                if (TestOptions.ContainerFixture is { })
                {
                    var schemaName = NewSchemaName;
                    options.ConnectionString = $"{TestOptions.ContainerFixture.ConnectionString};Search Path={schemaName}";
                    options.DatabaseSchemaName = schemaName;
                }
                if (TestOptions.PathToEnvironment is { })
                {
                    options.PathToEnvironment = TestOptions.PathToEnvironment;
                }
            })
            .CreateClient();
    }

    public StringContent EmptyContent { get; } =
        new StringContent(string.Empty, Encoding.UTF8, "application/json");

    private string NewSchemaName =>
        string.Format(SCHEMA_FORMAT, Guid.NewGuid());

}

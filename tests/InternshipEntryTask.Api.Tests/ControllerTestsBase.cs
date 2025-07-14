using InternshipEntryTask.Api.Tests.Base;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace InternshipEntryTask.Api.Tests;

public class ControllerTestsBase : IClassFixture<PostgreSqlFixture>
{
    private const string SCHEMA_FORMAT = "test_{0}";
    private readonly PostgreSqlFixture _fixture;

    public ControllerTestsBase(PostgreSqlFixture fixture)
    {
        _fixture = fixture;
    }

    public JsonSerializerOptions DefaultSerializerOptions { get; } = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public HttpClient CreateIsolatedClient() =>
        new CustomWebApplicationFactory(_fixture.ConnectionString, NewSchemaName).CreateClient();
    
    public StringContent EmptyContent { get; } = 
        new StringContent(string.Empty, Encoding.UTF8, "application/json");

    private string NewSchemaName =>
        string.Format(SCHEMA_FORMAT, Guid.NewGuid());

}

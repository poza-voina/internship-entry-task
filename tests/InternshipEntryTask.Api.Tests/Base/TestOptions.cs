using InternshipEntryTask.Api.Tests.Base;

namespace InternshipEntryTask.Api.IntegrationTests.Base;

public class TestOptions
{
    public string? PathToEnvironment { get; set; }
    public string? DatabaseSchemaName { get; set; }
    public IContainerFixture? ContainerFixture { get; set; } 
}

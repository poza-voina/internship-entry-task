using FluentAssertions;
using InternshipEntryTask.Abstractions.Exceptions;
using InternshipEntryTask.Api.Tests;
using Xunit;

namespace InternshipEntryTask.Api.IntegrationTests;

public class InitializeApplicationTests : ControllerTestsBase
{
    public InitializeApplicationTests()
    {
    }

    [Fact]
    public void Initialize_WhenGameEnvironmentSettingsNonValid_ThrowException()
    {
        Action act = () => CreateIsolatedClient(new() { PathToEnvironment = "TestConfigs/appsettings.invalid.json" });

        act.Should().Throw<EnvironmentConfigurationException>();
    }

}

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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void Initialize_WhenGameEnvironmentSettingsNonValid_ThrowException(int caseNumber)
    {
        Action act = () => CreateIsolatedClient(new() { PathToEnvironment = $"TestConfigs/appsettings.invalid.gamesettings.case{caseNumber}.json" });

        act.Should().Throw<EnvironmentConfigurationException>();
    }
}

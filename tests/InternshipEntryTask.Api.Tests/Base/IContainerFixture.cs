using Xunit;

namespace InternshipEntryTask.Api.Tests.Base;

public interface IContainerFixture : IAsyncLifetime
{
    public string ConnectionString { get; }
}
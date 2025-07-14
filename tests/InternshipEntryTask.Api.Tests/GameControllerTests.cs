using FluentAssertions;
using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace InternshipEntryTask.Api.Tests;

public class GameControllerTests : ControllerTestsBase
{    
    public GameControllerTests(PostgreSqlFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreateGame_WhenGameDoesNotExists_ReturnNewGame()
    {
        // Arrange
        var client = CreateIsolatedClient();
        GameDto expectedData = new GameDto
        {
            Id = 1,
            NextMove = CellValue.X,
            Status = GameStatus.Progress,
            Winner = null,
            Width = 3,
            Height = 3,
            JoinKey = Guid.NewGuid(),
            AccessKey = Guid.NewGuid(),
            Moves = []
        };

        var emptyContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/games", emptyContent);
        var actualData = await response.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actualData.Should()
            .BeEquivalentTo(
                expectedData,
                options => options.ComparingByMembers<GameDto>()
                .Excluding(x => x.JoinKey)
                .Excluding(x => x.AccessKey)
            );
    }

    [Fact]
    public async Task GetGame_WhenGameExists_ReturnGame()
    {
        // Arrange
        var client = CreateIsolatedClient();
        GameDto expectedData = new GameDto
        {
            Id = 1,
            NextMove = CellValue.X,
            Status = GameStatus.Progress,
            Winner = null,
            Width = 3,
            Height = 3,
            JoinKey = Guid.NewGuid(),
            AccessKey = Guid.NewGuid(),
            Moves = []
        };

        var emptyContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

        // Act
        await client.PostAsync("/games", emptyContent);
        var response = await client.GetAsync("/games/1");
        var actualData = await response.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actualData.Should()
            .BeEquivalentTo(
                expectedData,
                options => options.ComparingByMembers<GameDto>()
                .Excluding(x => x.JoinKey)
                .Excluding(x => x.AccessKey)
            );
    }
}
using FluentAssertions;
using InternshipEntryTask.Api.Tests.Base;
using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Enums;
using System.Net;
using System.Net.Http.Json;
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
            Moves = []
        };

        // Act
        var response = await client.PostAsync("/games", EmptyContent);
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

        actualData.JoinKey.Should().NotBe(Guid.Empty);
        actualData.JoinKey.Should().NotBeNull();
        actualData.AccessKey.Should().Be(null);
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

        // Act
        await client.PostAsync("/games", EmptyContent);
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

        actualData.JoinKey.Should().BeNull();
        actualData.AccessKey.Should().BeNull();
    }

    [Fact]
    public async Task GetGame_WhenGameNonExists_Return404Error()
    {
        // Arrange
        var client = CreateIsolatedClient();

        // Act
        var response = await client.GetAsync("/games/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task JoinGame_WhenGameExistsAndJoinedTwoPlayers_ReturnOk()
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

        var createGameResponse = await client.PostAsync("/games", EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequestFirstPlayer = new HttpRequestBuilder(HttpMethod.Post, "games/join")
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader("X-Join-Key", joinkey)
            .Build();

        var joinRequestSecondPlayer = new HttpRequestBuilder(HttpMethod.Post, "games/join")
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.O })
            .WithHeader("X-Join-Key", joinkey)
            .Build();

        // Act
        var firstJoinGameResponse = await client.SendAsync(joinRequestFirstPlayer);
        var secondJoinGameResponse = await client.SendAsync(joinRequestSecondPlayer);

        var firstActualData = await firstJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);
        var secondActualData = await secondJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);

        // Assert
        firstActualData.Should()
            .BeEquivalentTo(
                expectedData,
                options => options.ComparingByMembers<GameDto>()
                .Excluding(x => x.JoinKey)
                .Excluding(x => x.AccessKey)
            );

        secondActualData.Should()
            .BeEquivalentTo(
                expectedData,
                options => options.ComparingByMembers<GameDto>()
                .Excluding(x => x.JoinKey)
                .Excluding(x => x.AccessKey)
            );

        firstJoinGameResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        firstActualData.AccessKey.Should().NotBeNull();
        firstActualData.AccessKey.Should().NotBe(Guid.Empty);
        firstActualData.JoinKey.Should().BeNull();

        firstJoinGameResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        secondActualData.AccessKey.Should().NotBeNull();
        secondActualData.AccessKey.Should().NotBe(Guid.Empty);
        secondActualData.JoinKey.Should().BeNull();
    }

    [Fact]
    public async Task JoinGame_WhenGameExistsAndJoinedTwoPlayers_ReturnBadRequest()
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

        var createGameResponse = await client.PostAsync("/games", EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequestFirstPlayer = new HttpRequestBuilder(HttpMethod.Post, "games/join")
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader("X-Join-Key", joinkey)
            .Build();

        var joinRequestSecondPlayer = new HttpRequestBuilder(HttpMethod.Post, "games/join")
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader("X-Join-Key", joinkey)
            .Build();

        // Act
        var firstJoinGameResponse = await client.SendAsync(joinRequestFirstPlayer);
        var secondJoinGameResponse = await client.SendAsync(joinRequestSecondPlayer);

        var firstActualData = await firstJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);
        var secondActualData = await secondJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);

        // Assert
        secondJoinGameResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Move_WhenMoveOutOfRange_ReturnBadRequest()
    {
        // Arrange
        var client = CreateIsolatedClient();

        var incorrectMoves = new List<MoveRequest> {
            new MoveRequest { Row = -1, Column = 0},
            new MoveRequest { Row = 0, Column = -1},
            new MoveRequest { Row = 2000, Column = 0},
            new MoveRequest { Row = 0, Column = 2000},
        };


        var createGameResponse = await client.PostAsync("/games", EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequest = new HttpRequestBuilder(HttpMethod.Post, "games/join")
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader("X-Join-Key", joinkey)
            .Build();

        var joinResponse = await client.SendAsync(joinRequest);

        var accessKey = (await joinResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.AccessKey.ToString()!;

        var requests = incorrectMoves.Select(x =>
            new HttpRequestBuilder(HttpMethod.Post, "games/move")
                .WithJsonContent(x)
                .WithHeader("X-Access-Key", accessKey)
                .Build()
        ).ToList();

        // Act
        var responses = new List<HttpResponseMessage>();
        foreach(var item in requests)
        {
            responses.Add(await client.SendAsync(item));
        }

        // Assert
        foreach (var response in responses)
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }

    [Theory]
    [ClassData(typeof(GameMovesData))]
    public async Task Move_WhenPlayGame_ReturnValidGameResultState(GameDto expectedGame, List<MoveRequest> moves)
    {
        // Arrange
        var client = CreateIsolatedClient();

        var createGameResponse = await client.PostAsync("/games", EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequestFirstPlayer = new HttpRequestBuilder(HttpMethod.Post, "games/join")
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader("X-Join-Key", joinkey)
            .Build();

        var joinRequestSecondPlayer = new HttpRequestBuilder(HttpMethod.Post, "games/join")
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.O })
            .WithHeader("X-Join-Key", joinkey)
            .Build();

        var firstJoinGameResponse = await client.SendAsync(joinRequestFirstPlayer);
        var secondJoinGameResponse = await client.SendAsync(joinRequestSecondPlayer);

        string[] accessKeys =
            [
                (await firstJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.AccessKey.ToString()!,
                (await secondJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.AccessKey.ToString()!
            ];

        var requests = moves.Select((move, index) =>
            new HttpRequestBuilder(HttpMethod.Post, "games/move")
                .WithJsonContent(move)
                .WithHeader("X-Access-Key", accessKeys[index % accessKeys.Length])
                .Build()
        ).ToList();

        // Act
        foreach (var item in requests)
        {
            var moveResponse = await client.SendAsync(item);
            moveResponse.EnsureSuccessStatusCode();
        }

        var gameResponse = await client.GetAsync("games/1");

        var actualGame = await gameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);

        // Assert
        actualGame.Should()
        .BeEquivalentTo(
            expectedGame,
            options => options.ComparingByMembers<GameDto>()
            .Excluding(x => x.JoinKey)
            .Excluding(x => x.AccessKey)
        );
    }

    public class GameMovesData : TheoryData<GameDto, List<MoveRequest>>
    {
        public GameMovesData()
        {
            Add(
                new GameDto
                {
                    Id = 1,
                    NextMove = CellValue.O,
                    Status = GameStatus.Finished,
                    Winner = WinnerStatus.X,
                    Width = 3,
                    Height = 3,
                    JoinKey = null,
                    AccessKey = null,
                },

                new List<MoveRequest>
                {
                    new MoveRequest {Row = 0, Column = 0},
                    new MoveRequest {Row = 1, Column = 0},
                    new MoveRequest {Row = 0, Column = 1},
                    new MoveRequest {Row = 1, Column = 1},
                    new MoveRequest {Row = 0, Column = 2},
                });


            Add(
                new GameDto
                {
                    Id = 1,
                    NextMove = CellValue.X,
                    Status = GameStatus.Finished,
                    Winner = WinnerStatus.O,
                    Width = 3,
                    Height = 3,
                    JoinKey = null,
                    AccessKey = null,
                },
                new List<MoveRequest>
                {
                    new MoveRequest { Row = 0, Column = 0 },
                    new MoveRequest { Row = 0, Column = 1 },
                    new MoveRequest { Row = 1, Column = 0 },
                    new MoveRequest { Row = 1, Column = 1 },
                    new MoveRequest { Row = 2, Column = 2 },
                    new MoveRequest { Row = 2, Column = 1 },
                }
            );

            Add(
                new GameDto
                {
                    Id = 1,
                    NextMove = CellValue.O,
                    Status = GameStatus.Finished,
                    Winner = WinnerStatus.Draw,
                    Width = 3,
                    Height = 3,
                    JoinKey = null,
                    AccessKey = null,
                },
                new List<MoveRequest>
                {
                    new MoveRequest { Row = 0, Column = 0 },
                    new MoveRequest { Row = 0, Column = 1 },
                    new MoveRequest { Row = 0, Column = 2 },
                    new MoveRequest { Row = 1, Column = 1 },
                    new MoveRequest { Row = 1, Column = 0 },
                    new MoveRequest { Row = 1, Column = 2 },
                    new MoveRequest { Row = 2, Column = 1 },
                    new MoveRequest { Row = 2, Column = 0 },
                    new MoveRequest { Row = 2, Column = 2 },
                }
            );
        }
    }
}
using FluentAssertions;
using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Api.IntegrationTests;
using InternshipEntryTask.Api.IntegrationTests.Base;
using InternshipEntryTask.Api.Tests.Base;
using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Enums;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace InternshipEntryTask.Api.Tests;

public class GameControllerTests(PostgreSqlFixture fixture) : ControllerTestsBase, IClassFixture<PostgreSqlFixture>
{
    private const string VERSION = "v1";
    private const string PATH_TO_GAMES = VERSION + "/games";
    private const string PATH_TO_GAME_FORMAT = PATH_TO_GAMES + "/{0}";
    private const string PATH_TO_GAME_JOIN = PATH_TO_GAMES + "/join";
    private const string PATH_TO_GAME_MOVE_FORMAT = PATH_TO_GAMES + "/{0}/move";

    private IsolatedClientOptions DefaultIsolatedClientOptions { get; } = new() { ContainerFixture = fixture, PathToEnvironment = "TestConfigs/appsettings.test.json" };

    [Fact]
    public async Task CreateGame_WhenGameDoesNotExists_ReturnNewGame()
    {
        // Arrange
        var client = CreateIsolatedClient(DefaultIsolatedClientOptions);
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
        var response = await client.PostAsync(PATH_TO_GAMES, EmptyContent);
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
        var client = CreateIsolatedClient(DefaultIsolatedClientOptions);
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
        await client.PostAsync(PATH_TO_GAMES, EmptyContent);
        var response = await client.GetAsync(string.Format(PATH_TO_GAME_FORMAT, 1));
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
        var client = CreateIsolatedClient(DefaultIsolatedClientOptions);

        // Act
        var response = await client.GetAsync(string.Format(PATH_TO_GAME_FORMAT, 1));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task JoinGame_WhenGameExistsAndJoinedTwoPlayers_ReturnOk()
    {
        // Arrange 
        var client = CreateIsolatedClient(DefaultIsolatedClientOptions);
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

        var createGameResponse = await client.PostAsync(PATH_TO_GAMES, EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequestFirstPlayer = new HttpRequestBuilder(HttpMethod.Post, PATH_TO_GAME_JOIN)
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader(HeadersKeysConstants.JOIN_KEY, joinkey)
            .Build();

        var joinRequestSecondPlayer = new HttpRequestBuilder(HttpMethod.Post, PATH_TO_GAME_JOIN)
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.O })
            .WithHeader(HeadersKeysConstants.JOIN_KEY, joinkey)
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
        var client = CreateIsolatedClient(DefaultIsolatedClientOptions);
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

        var createGameResponse = await client.PostAsync(PATH_TO_GAMES, EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequestFirstPlayer = new HttpRequestBuilder(HttpMethod.Post, PATH_TO_GAME_JOIN)
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader(HeadersKeysConstants.JOIN_KEY, joinkey)
            .Build();

        var joinRequestSecondPlayer = new HttpRequestBuilder(HttpMethod.Post, PATH_TO_GAME_JOIN)
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader(HeadersKeysConstants.JOIN_KEY, joinkey)
            .Build();

        // Act
        var firstJoinGameResponse = await client.SendAsync(joinRequestFirstPlayer);
        var secondJoinGameResponse = await client.SendAsync(joinRequestSecondPlayer);

        var firstActualData = await firstJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);
        var secondActualData = await secondJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions);

        // Assert
        secondJoinGameResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Move_WhenMoveOutOfRange_ReturnBadRequest()
    {
        // Arrange
        var client = CreateIsolatedClient(DefaultIsolatedClientOptions);

        var incorrectMoves = new List<MoveRequest> {
            new MoveRequest { Row = -1, Column = 0},
            new MoveRequest { Row = 0, Column = -1},
            new MoveRequest { Row = 2000, Column = 0},
            new MoveRequest { Row = 0, Column = 2000},
        };


        var createGameResponse = await client.PostAsync(PATH_TO_GAMES, EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequest = new HttpRequestBuilder(HttpMethod.Post, PATH_TO_GAME_JOIN)
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader(HeadersKeysConstants.JOIN_KEY, joinkey)
            .Build();

        var joinResponse = await client.SendAsync(joinRequest);

        var accessKey = (await joinResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.AccessKey.ToString()!;

        var requests = incorrectMoves.Select(x =>
            new HttpRequestBuilder(HttpMethod.Post, string.Format(PATH_TO_GAME_MOVE_FORMAT, 1))
                .WithJsonContent(x)
                .WithHeader(HeadersKeysConstants.ACCESS_KEY, accessKey)
                .Build()
        ).ToList();

        // Act
        var responses = new List<HttpResponseMessage>();
        foreach (var item in requests)
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
    public async Task Move_WhenPlayGame_ReturnValidGameResultState(GameDto expectedGame, List<MoveRequest> moves, string? appsettings)
    {
        // Arrange
        HttpClient client;
        if (appsettings is { })
        {
            client = CreateIsolatedClient(new() { PathToEnvironment = appsettings, ContainerFixture = fixture });
        }
        else
        {
            client = CreateIsolatedClient(DefaultIsolatedClientOptions);
        }

        var createGameResponse = await client.PostAsync(PATH_TO_GAMES, EmptyContent);
        var joinkey = (await createGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.JoinKey.ToString()!;

        var joinRequestFirstPlayer = new HttpRequestBuilder(HttpMethod.Post, PATH_TO_GAME_JOIN)
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.X })
            .WithHeader(HeadersKeysConstants.JOIN_KEY, joinkey)
            .Build();

        var joinRequestSecondPlayer = new HttpRequestBuilder(HttpMethod.Post, PATH_TO_GAME_JOIN)
            .WithJsonContent(new JoinRequest { PlayerSymbol = CellValue.O })
            .WithHeader(HeadersKeysConstants.JOIN_KEY, joinkey)
            .Build();

        var firstJoinGameResponse = await client.SendAsync(joinRequestFirstPlayer);
        var secondJoinGameResponse = await client.SendAsync(joinRequestSecondPlayer);

        string[] accessKeys =
            [
                (await firstJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.AccessKey.ToString()!,
                (await secondJoinGameResponse.Content.ReadFromJsonAsync<GameDto>(DefaultSerializerOptions))!.AccessKey.ToString()!
            ];

        var requests = moves.Select((move, index) =>
            new HttpRequestBuilder(HttpMethod.Post, string.Format(PATH_TO_GAME_MOVE_FORMAT, 1))
                .WithJsonContent(move)
                .WithHeader(HeadersKeysConstants.ACCESS_KEY, accessKeys[index % accessKeys.Length])
                .Build()
        ).ToList();

        // Act
        foreach (var item in requests)
        {
            var moveResponse = await client.SendAsync(item);
            moveResponse.EnsureSuccessStatusCode();
        }

        var gameResponse = await client.GetAsync(string.Format(PATH_TO_GAME_FORMAT, 1));

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

    public class GameMovesData : TheoryData<GameDto, List<MoveRequest>, string?>
    {
        public record MovePosition
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public static List<MoveRequest> ToMoveRequests(List<MovePosition> movePositions)
            {
                return movePositions.Select(x => new MoveRequest { Row = x.Row, Column = x.Column }).ToList();
            }

            public static List<MoveDto> ToGameMoves(List<MovePosition> movePositions)
            {
                return movePositions.Select(
                    (x, index) => new MoveDto
                    {
                        Row = x.Row,
                        Column = x.Column,
                        CellValue = index % 2 == 0 ? CellValue.X : CellValue.O
                    }).ToList();
            }
        }

        public GameMovesData()
        {

            var moves = new List<MovePosition>
                {
                    new MovePosition {Row = 0, Column = 0},
                    new MovePosition {Row = 1, Column = 0},
                    new MovePosition {Row = 0, Column = 1},
                    new MovePosition {Row = 1, Column = 1},
                    new MovePosition {Row = 0, Column = 2},
                };

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
                    Moves = MovePosition.ToGameMoves(moves)
                },
                MovePosition.ToMoveRequests(moves),
                null);

            moves = new List<MovePosition>
                {
                    new MovePosition { Row = 0, Column = 0 },
                    new MovePosition { Row = 0, Column = 1 },
                    new MovePosition { Row = 1, Column = 0 },
                    new MovePosition { Row = 1, Column = 1 },
                    new MovePosition { Row = 2, Column = 2 },
                    new MovePosition { Row = 2, Column = 1 },
                };

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
                    Moves = MovePosition.ToGameMoves(moves)
                },
                MovePosition.ToMoveRequests(moves),
                null);

            moves = new List<MovePosition>()
                {
                    new MovePosition { Row = 0, Column = 0 },
                    new MovePosition { Row = 0, Column = 1 },
                    new MovePosition { Row = 0, Column = 2 },
                    new MovePosition { Row = 1, Column = 1 },
                    new MovePosition { Row = 1, Column = 0 },
                    new MovePosition { Row = 1, Column = 2 },
                    new MovePosition { Row = 2, Column = 1 },
                    new MovePosition { Row = 2, Column = 0 },
                    new MovePosition { Row = 2, Column = 2 },
                };

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
                    Moves = MovePosition.ToGameMoves(moves)
                },
                MovePosition.ToMoveRequests(moves),
                null
            );

            moves = new List<MovePosition>
                {
                    new MovePosition { Row = 10, Column = 9 },
                    new MovePosition { Row = 5,  Column = 5 },
                    new MovePosition { Row = 10, Column = 10 },
                    new MovePosition { Row = 6,  Column = 5 },
                    new MovePosition { Row = 10, Column = 11 }
                };

            Add(
                new GameDto
                {
                    Id = 1,
                    NextMove = CellValue.O,
                    Status = GameStatus.Finished,
                    Winner = WinnerStatus.X,
                    Width = 20,
                    Height = 20,
                    JoinKey = null,
                    AccessKey = null,
                    Moves = MovePosition.ToGameMoves(moves)
                },
                MovePosition.ToMoveRequests(moves),
                "TestConfigs/appsettings.test.big.game.json");
        }
    }
}
using InternshipEntryTask.Core.Game;
using InternshipEntryTask.Infrastructure.Enums;
using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Abstractions.Exceptions;
using Xunit;
using InternshipEntryTask.Infrastructure.Models;
using FluentAssertions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System.ComponentModel.DataAnnotations;

namespace InternshipEntryTask.Core.UnitTests;

public class GameBoardEvaluatorTests
{
    [Fact]
    public void Move_WhenPositionOutOfRange_ShouldThrow()
    {
        // Arrange
        var board = new BoardDto
        {
            Column = 5,
            Row = 5,
            GameSettings = new GameSettings { Width = 3, Height = 3, WinLength = 3, Chance = 100 },
            Moves = [],
            NextMove = CellValue.X
        };

        var evaluator = new GameBoardEvaluator(board);

        // Act
        Action act = () => evaluator.Move();

        // Assert
        act.Should()
           .Throw<ValidationException>()
           .WithMessage(MessagesConstants.BOARD_OUT_OF_RANGE_ERROR_MESSAGE);
    }

    [Fact]
    public void Move_WhenMoveAlreadyExists_ShouldThrow()
    {
        // Arrange
        var board = new BoardDto
        {
            Column = 0,
            Row = 0,
            GameSettings = new GameSettings { Width = 3, Height = 3, WinLength = 3, Chance = 100 },
            Moves = [new MoveModel { Column = 0, Row = 0, CellValue = CellValue.X }],
            NextMove = CellValue.O
        };

        var evaluator = new GameBoardEvaluator(board);

        // Act
        Action act = () => evaluator.Move();

        // Assert
        act.Should()
           .Throw<ConflictException>();
    }

    [Fact]
    public void Move_WhenRowCompleted_ShouldReturnWin()
    {
        // Arrange
        var board = new BoardDto
        {
            Column = 2,
            Row = 0,
            GameSettings = new GameSettings { Width = 3, Height = 3, WinLength = 3, Chance = 100 },
            Moves = [
                new MoveModel { Column = 0, Row = 0, CellValue = CellValue.X },
                new MoveModel { Column = 1, Row = 0, CellValue = CellValue.X },
            ],
            NextMove = CellValue.X
        };

        var evaluator = new GameBoardEvaluator(board);

        // Act
        var result = evaluator.Move();

        // Assert
        result.Winner.Should().Be(WinnerStatus.X);
        result.MoveIndex.Should().Be(2);
    }

    [Fact]
    public void Move_WhenBoardIsFull_ShouldReturnDraw()
    {
        // Arrange
        var board = new BoardDto
        {
            Column = 2,
            Row = 2,
            GameSettings = new GameSettings { Width = 3, Height = 3, WinLength = 3, Chance = 100 },
            Moves = [

                new MoveModel() { Column = 0, Row = 0, CellValue = CellValue.X },
                new MoveModel() { Column = 0, Row = 1, CellValue = CellValue.O},
                new MoveModel() { Column = 0, Row = 2, CellValue = CellValue.X},
                new MoveModel() { Column = 1, Row = 0, CellValue = CellValue.X},
                new MoveModel() { Column = 1, Row = 1, CellValue = CellValue.O},
                new MoveModel() { Column = 1, Row = 2, CellValue = CellValue.O},
                new MoveModel() { Column = 2, Row = 0, CellValue = CellValue.O},
                new MoveModel() { Column = 2, Row = 1, CellValue = CellValue.X},
            ],
            NextMove = CellValue.X
        };

        var evaluator = new GameBoardEvaluator(board);

        // Act
        var result = evaluator.Move();

        // Assert
        result.Winner.Should().Be(WinnerStatus.Draw);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Move_WhenChanceIs100_ShouldToggleMove(int chanceTurnNumber)
    {
        // Arrange
        List<BoardDto> boards = new List<BoardDto>();
        List<MoveModel> moves = new List<MoveModel>();

        for (int i = 0; i < chanceTurnNumber; i++)
        {
            boards.Add(
                new BoardDto
                {
                    Row = 0,
                    Column = i,
                    GameSettings = new GameSettings
                    {
                        Width = chanceTurnNumber,
                        Height = chanceTurnNumber,
                        WinLength = chanceTurnNumber,
                        Chance = 100,
                        ChanceTurnNumber = chanceTurnNumber
                    },
                    Moves = moves.ToList(),
                    NextMove = CellValue.X
                }
            );

            MoveModel move = new MoveModel()
            {
                Row = 0,
                Column = i,
            };

            moves.Add(move);
        }

        IEnumerable<(int Index, BoardResultDto MoveResult)> results = boards.Select((x, index) => (index, new GameBoardEvaluator(x).Move()));

        foreach (var item in results)
        {
            if ((item.Index + 1) % chanceTurnNumber == 0)
            {
                item.MoveResult.CellValue.Should().Be(CellValue.O);
            }
            else
            {
                item.MoveResult.CellValue.Should().Be(CellValue.X);
            }
        }
    }
}

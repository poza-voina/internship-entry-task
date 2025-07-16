using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Abstractions.Exceptions;
using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Core.Game.Interfaces;
using InternshipEntryTask.Infrastructure.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternshipEntryTask.Core.Game;

/// <inheritdoc cref="BoardDto"/>
public class GameBoardEvaluator(BoardDto board) : IGameBoardEvaluator
{
	/// <summary>
	/// Доска
	/// </summary>
	public BoardDto Board { get; private init; } = board;

	private Dictionary<(int Column, int Row), CellValue>? _groupedMoves;
	
	/// <inheritdoc/>
	public BoardResultDto Move()
	{
		if (Board.Row < 0 || Board.Column < 0 || Board.Row >= Board.GameSettings.Height || Board.Column >= Board.GameSettings.Width)
		{
			throw new ValidationException(MessagesConstants.BOARD_OUT_OF_RANGE_ERROR_MESSAGE);
		}

		if (Board.Moves.Any(x => x.Column == Board.Column && x.Row == Board.Row))
		{
			throw new ConflictException(string.Format(MessagesConstants.MOVE_ALREADY_EXISTS_MESSAGE_FORMAT_ERROR_MESSAGE, Board.Column, Board.Row));
		}

		_groupedMoves = Board.Moves.ToDictionary(x => (x.Column, x.Row), x => x.CellValue);

		var prevCountMoves = Board.Moves.Count();

		return new BoardResultDto
		{
			CellValue = ToggleCellValueWithChance(prevCountMoves + 1),
			NextMove = ToggleCellValue(Board.NextMove),
			MoveIndex = prevCountMoves,
			Row = Board.Row,
			Column = Board.Column,
			Winner = CheckWin() ?? CheckDraw(prevCountMoves + 1)
		};
	}

	private CellValue ToggleCellValueWithChance(int currentCountMoves) =>
		currentCountMoves != Board.GameSettings.ChanceTurnNumber ? 
			Board.NextMove :
			new Random().Next(100) < Board.GameSettings.Chance ? ToggleCellValue(Board.NextMove) : Board.NextMove;

	private CellValue ToggleCellValue(CellValue cellValue)
	{
		return cellValue switch
		{
			CellValue.X => CellValue.O,
			CellValue.O => CellValue.X,
			_ => throw new InvalidEnumArgumentException(nameof(Board.NextMove), (int)Board.NextMove, typeof(CellValue)),
		};
	}

	private int CountInDirection(int directionColumn, int directionRow)
	{
		ArgumentNullException.ThrowIfNull(_groupedMoves);

		int count = 0;

		for (int step = 1; step < Board.GameSettings.WinLength; step++)
		{
			int newColumn = Board.Column + directionColumn * step;
			int newRow = Board.Row + directionRow * step;

			if (!_groupedMoves.TryGetValue((newColumn, newRow), out CellValue val) || val != Board.NextMove)
			{
				break;
			}

			count++;
		}

		return count;
	}

	private WinnerStatus? CheckWin()
	{
		int[][] directions = new int[][]
		{
			new int[] {1, 0},
			new int[] {0, 1},
			new int[] {1, 1},
			new int[] {1, -1}
		};

		foreach (var dir in directions)
		{
			int count = 1;

			count += CountInDirection(dir[0], dir[1]);
			count += CountInDirection(-dir[0], -dir[1]);

			if (count >= Board.GameSettings.WinLength)
			{
				return Board.NextMove switch
				{
					CellValue.X => WinnerStatus.X,
					CellValue.O => WinnerStatus.O,
					_ => throw new InvalidEnumArgumentException(nameof(Board.NextMove), (int)Board.NextMove, typeof(CellValue))
				};
			}
		}

		return null;
	}

	private WinnerStatus? CheckDraw(int countMoves)
	{
		int totalCells = Board.GameSettings.Width * Board.GameSettings.Height;

		if (countMoves >= totalCells)
		{
			return WinnerStatus.Draw;
		}

		return null;
	}
}
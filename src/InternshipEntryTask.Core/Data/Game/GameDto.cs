using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Core.Data.Game;

/// <summary>
/// Модель DTO "Игра"
/// </summary>
public class GameDto
{
    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Текущий игрок, который должен сделать ход ('X' или 'O')
    /// </summary>
    public CellValue NextMove { get; set; }

    /// <summary>
    /// Статус игры
    /// </summary>
    public GameStatus Status { get; set; }

    /// <summary>
    /// Статус победы
    /// </summary>
    public WinnerStatus? Winner { get; set; }

    /// <summary>
    /// Ширина поля
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Высота поля
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Ключ для присоединия к игре
    /// </summary>
    public Guid? JoinKey { get; set; }

    /// <summary>
    /// Ключ авторизации игрока
    /// </summary>
    public Guid? AccessKey { get; set; }

    /// <summary>
    /// Доска
    /// </summary>
    public char[][]? Board { get; set; }

    /// <summary>
    /// Список ходов
    /// </summary>
    public IEnumerable<MoveDto> Moves { get; set; } = [];
}
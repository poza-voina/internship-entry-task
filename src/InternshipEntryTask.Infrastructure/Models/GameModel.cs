using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Infrastructure.Models;

/// <summary>
/// Сущность БД Игра
/// </summary>
public class GameModel : IDatabaseModel
{
    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Токен доступа игра X
    /// </summary>
    public Guid? AccessKeyPlayerX { get; set; }

    /// <summary>
    /// Токен доступа игрока O
    /// </summary>
    public Guid? AccessKeyPlayerO { get; set; }

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
    /// Ключ добавление игрока к игре
    /// </summary>
    public Guid JoinKey { get; set; }

    /// <summary>
    /// Навигационное поле
    /// </summary>
    public virtual ICollection<MoveModel> Moves { get; set; } = [];
}
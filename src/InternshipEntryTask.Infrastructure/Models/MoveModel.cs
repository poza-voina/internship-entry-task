using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Infrastructure.Models;

/// <summary>
/// Сущность БД Ход
/// </summary>
public class MoveModel : IDatabaseModel
{
    /// <summary>
    /// Идентификатор хода
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор игрока
    /// </summary>
    public Guid AccessKeyPlayer { get; set; }
    
    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public long GameId { get; set; }
    
    /// <summary>
    /// Значение на поле игры
    /// </summary>
    public CellValue CellValue { get; set; }

    /// <summary>
    /// Индекс хода
    /// </summary>
    public int MoveIndex { get; set; }

    /// <summary>
    /// Индекс строки
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Индекс колонки
    /// </summary>
    public int Column { get; set; }
    
    /// <summary>
    /// Навигационное поле игра
    /// </summary>
    public virtual GameModel? Game { get; set; }
}

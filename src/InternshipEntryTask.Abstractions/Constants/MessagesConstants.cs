namespace InternshipEntryTask.Abstractions.Constants;

/// <summary>
/// Константы с сообщениями
/// </summary>
public class MessagesConstants
{
    /// <summary>
    /// Формат сообщения "Игра не найдена"
    /// </summary>
    public const string GAME_NOTFOUND_ERROR_MESSAGE_FORMAT = "Игра с JoinKey = {0} не найдена";

    /// <summary>
    /// Сообщение "Игра не найдена"
    /// </summary>
    public const string GAME_NOT_FOUND_WITH_ACCESSKEY_ERROR_MESSAGE = "Игра не найдена для указанного ключа доступа";
    
    /// <summary>
    /// Сообщение "Не ваш ход"
    /// </summary>
    public const string NOT_YOUR_TURN_ERROR_MESSAGE = "Сейчас не ваш ход";
    
    /// <summary>
    /// Сообщение о завершении игры
    /// </summary>
    public const string GAME_IS_FINISHED_ERROR_MESSAGE = "Игра уже закончилась";
    
    /// <summary>
    /// Сообщение о не найденной переменное в окружении
    /// </summary>
    public const string ENVIROMENT_ERROR_MESSAGE_FORMAT = "'{0}:{1}' не найдено";
    
    /// <summary>
    /// Сообщение "Выход за пределы доски"
    /// </summary>
    public const string BOARD_OUT_OF_RANGE_ERROR_MESSAGE = "Вы вышли за пределы поля";
    
    /// <summary>
    /// Формат сообщения о существующем ходе
    /// </summary>
    public const string MOVE_ALREADY_EXISTS_MESSAGE_FORMAT_ERROR_MESSAGE = "Ход ({0}, {1}) уже существует";
    
    /// <summary>
    /// Сообщение о существовании игрока X
    /// </summary>
    public const string PLAYER_X_ALREADY_EXISTS_MESSAGE = "Игрок Х уже существует";

    /// <summary>
    /// Сообщение о существовании игрока O
    /// </summary>
    public const string PLAYER_O_ALREADY_EXISTS_MESSAGE = "Игрок O уже существует";

    /// <summary>
    /// Сообщение о неправильном размере поля
    /// </summary>
    public const string ENV_WIDTH_HEIGHT_ERROR_MESSAGE_FORMAT = "Некорректные размеры поля. Ширина и высота должны быть в пределах [{0}, {1}]";

    /// <summary>
    /// Сообщение о неправильном условии выигрыша
    /// </summary>
    public const string ENV_WINLENGHT_ERROR_MESSAGE_FORMAT = "Некорректное условие выигрыша. Текущие размеры Ширина = {0} Высота = {1}";

    /// <summary>
    /// Не удалось найти сущность
    /// </summary>
    public const string CANT_FIND_ENTITY_ERROR_FORMAT = "Не удалось найти сущность с id = {0}";
}

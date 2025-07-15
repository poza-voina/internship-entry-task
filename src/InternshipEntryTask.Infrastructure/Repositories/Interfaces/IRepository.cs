using InternshipEntryTask.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipEntryTask.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Базовый репозитория
/// </summary>
/// <typeparam name="TModel">Модель БД</typeparam>
public interface IRepository<TModel> where TModel : class, IDatabaseModel
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    DbContext Context { get; }

    /// <summary>
    /// Добавить запись в БД
    /// </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленная сущности</returns>
    Task<TModel> AddAsync(
        TModel entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить несколько записей в БД
    /// </summary>
    /// <param name="entities">Коллекция сущностей</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения задачи</returns>
    Task AddRangeAsync(
        IEnumerable<TModel> entities,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить несколько записей в БД
    /// </summary>
    /// <param name="entities">Коллекция сущностей</param>
    /// <returns>Результат выполнения задачи</returns>
    Task AddRangeAsync(params TModel[] entities);

    /// <summary>
    /// Если ли подключение к БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see langword="true"/> - если да, иначе <see langword="false"/></returns>
    Task<bool> CanConnectToDbAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить запись в БД по Id
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения задачи</returns>
    Task DeleteAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить запись в БД
    /// </summary>
    /// <param name="entity">Сущность для удаления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения задачи</returns>
    Task DeleteAsync(
        TModel entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить несколько записей в БД
    /// </summary>
    /// <param name="entities">Коллекция сущностей для удаления</param>
    /// <returns>Результат выполнения задачи</returns>
    Task DeleteRangeAsync(params TModel[] entities);

    /// <summary>
    /// Найти сущность в БД по ключу
    /// </summary>
    /// <param name="objects">Ключ или ключи записи</param>
    /// <returns>Найденная сущность, если сущность не найдена, то <see langword="null"/></returns>
    Task<TModel> FindAsync(params object[] objects);

    /// <summary>
    /// Получить все записи из БД
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    IQueryable<TModel> GetAll();

    /// <summary>
    /// Обновить запись в БД
    /// </summary>
    /// <param name="entity">Сущность для обновления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная сущность</returns>
    Task<TModel> UpdateAsync(
        TModel entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить несколько записей в БД
    /// </summary>
    /// <param name="entities">Колекция сущностей</param>
    /// <returns>Результат выполнения задачи</returns>
    Task UpdateRangeAsync(params TModel[] entities);

    /// <summary>
    /// Выполнить sql запрос
    /// </summary>
    /// <param name="sqlQuery">Sql запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество затронутых строк</returns>
    Task<int> ExecuteSqlAsync(
        FormattableString sqlQuery,
        CancellationToken cancellationToken = default);
}
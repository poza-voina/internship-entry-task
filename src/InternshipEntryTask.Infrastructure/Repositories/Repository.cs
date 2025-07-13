using InternshipEntryTask.Abstractions.Exceptions;
using InternshipEntryTask.Infrastructure.Models;
using InternshipEntryTask.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternshipEntryTask.Infrastructure.Repositories;

public class Repository<TModel>(ApplicationDbContext dbContext) : IRepository<TModel> where TModel : class, IDatabaseModel
{
    private const string CANT_FIND_ENTITY_ERROR_FORMAT = "Не удалось найти сущность с id = {0}";

    /// <inheritdoc/>
    public DbContext Context => dbContext;

    /// <inheritdoc/>
    public async Task<TModel> AddAsync(TModel entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    /// <inheritdoc/>
    public async Task AddRangeAsync(
        IEnumerable<TModel> entities,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await dbContext.AddRangeAsync(entities, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        foreach (TModel entity in entities)
        {
            dbContext.Entry(entity).State = EntityState.Detached;
        }
    }

    /// <inheritdoc/>
    public async Task AddRangeAsync(params TModel[] entities) =>
        await AddRangeAsync(entities.AsEnumerable());

    /// <inheritdoc/>
    public Task<bool> CanConnectToDbAsync(CancellationToken cancellationToken = default) =>
        dbContext.Database.CanConnectAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id);

        await DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(
        TModel entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        dbContext.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public async Task DeleteRangeAsync(params TModel[] entities)
    {
        if (entities.Length > 0)
        {
            foreach (var entity in entities)
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
            }

            dbContext.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task<int> ExecuteSqlAsync(
        FormattableString sqlQuery,
        CancellationToken cancellationToken = default) =>
        await dbContext.Database.ExecuteSqlAsync(sqlQuery, cancellationToken);

    /// <inheritdoc/>
    public async Task<TModel> FindAsync(params object[] objects)
    {
        ArgumentNullException.ThrowIfNull(objects);
        var entity = await dbContext.FindAsync<TModel>(objects)
            ?? throw new EntityNotFoundException(string.Format(CANT_FIND_ENTITY_ERROR_FORMAT, string.Join(',', objects)));
        dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    /// <inheritdoc/>
    public IQueryable<TModel> GetAll() =>
        dbContext
            .Set<TModel>()
            .AsNoTracking();

    /// <inheritdoc/>
    public async Task<TModel> UpdateAsync(
        TModel entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (dbContext.Entry(entity).State is EntityState.Detached)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        dbContext.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    /// <inheritdoc/>
    public async Task UpdateRangeAsync(params TModel[] entities)
    {
        foreach (TModel entity in entities)
        {
            await UpdateAsync(entity);
        }
    }
}
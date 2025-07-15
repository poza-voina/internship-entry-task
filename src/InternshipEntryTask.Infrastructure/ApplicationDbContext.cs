using InternshipEntryTask.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InternshipEntryTask.Infrastructure;

/// <summary>
/// Контекст БД
/// </summary>
/// <param name="options"></param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица игры
    /// </summary>
    public DbSet<GameModel> Games {  get; set; }
    
    /// <summary>
    /// Таблица ходы
    /// </summary>
    public DbSet<MoveModel> Moves { get; set; }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

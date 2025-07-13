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
    public DbSet<GameModel> Games {  get; set; }
    public DbSet<MoveModel> Moves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

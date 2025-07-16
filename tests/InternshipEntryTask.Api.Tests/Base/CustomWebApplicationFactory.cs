using InternshipEntryTask.Api.IntegrationTests.Base;
using InternshipEntryTask.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipEntryTask.Api.Tests.Base;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    private CustomWebApplicationFactoryOptions FactoryOptions { get; set; } = new();
    private bool _disposed = false;

    public CustomWebApplicationFactory(Action<CustomWebApplicationFactoryOptions>? options = null)
    {
        options?.Invoke(FactoryOptions);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        if (FactoryOptions.PathToEnvironment is { })
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.Sources.Clear();
                configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(FactoryOptions.PathToEnvironment, optional: false, reloadOnChange: false);
            });
        }

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            if (FactoryOptions.ConnectionString is { } && FactoryOptions.DatabaseSchemaName is { })
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(FactoryOptions.ConnectionString));

                var serviceProvider = services.BuildServiceProvider();
                ExecuteInScope(context =>
                {
#pragma warning disable
                    context.Database.ExecuteSqlRaw($"CREATE SCHEMA IF NOT EXISTS \"{FactoryOptions.DatabaseSchemaName}\""); //NOTE: Тестовые схемы. Логика была такая: 1 контейнер один тестовый класс. Для изоляции тестов используются схемы.
#pragma warning restore                    
                    context.Database.Migrate();
                }, serviceProvider);
            } 
            else //NOTE: подменяет на контекст который ничего не делает. Ну я надеюсь на это. Было интересно можно ли так сделать. Хотел протестировать валидацию некорректных параметров для настроек игры.
            {
                services.AddSingleton(new DbContextOptions<ApplicationDbContext>());
                services.AddScoped<ApplicationDbContext>();
            }
        });
    }

    public ApplicationDbContext DbContext =>
         Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

    protected override void Dispose(bool disposing) //TODO: проверить будет ли работать удалиние схемы после выполнения теста или нет
    {
        if (!_disposed)
        {
            if (disposing && FactoryOptions.ConnectionString is { } && FactoryOptions.DatabaseSchemaName is { })
            {
                ExecuteInScope(context =>
                {
#pragma warning disable
                    context.Database.ExecuteSqlRaw($"DROP SCHEMA IF EXISTS \"{FactoryOptions.DatabaseSchemaName}\" CASCADE");
#pragma warning restore
                }); ;
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }

    private void ExecuteInScope(Action<ApplicationDbContext> action, ServiceProvider? provider = null)
    {
        var serviceProvider = provider ?? Services;

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        action(context);
    }
}
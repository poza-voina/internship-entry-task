using InternshipEntryTask.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;

namespace InternshipEntryTask.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    private readonly string _connectionString;
    private readonly string _schema;
    private bool _disposed = false;

    public CustomWebApplicationFactory(string connectionString, string schema)
    {
        _schema = schema;
        _connectionString = $"{connectionString};Search Path={_schema}";
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_connectionString));

            var serviceProvider = services.BuildServiceProvider();
            ExecuteInScope(context =>
            {
                context.Database.ExecuteSqlRaw($"CREATE SCHEMA IF NOT EXISTS \"{_schema}\"");
                context.Database.Migrate();
            }, serviceProvider);
        });
    }

    public ApplicationDbContext DbContext =>    
         Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                ExecuteInScope(context =>
                {
                    context.Database.ExecuteSqlRaw($"DROP SCHEMA IF EXISTS \"{_schema}\" CASCADE");
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
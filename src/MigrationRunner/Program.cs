using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Api.Extenctions;
using InternshipEntryTask.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .Build();

        var connectionSection = configuration.GetRequiredSection(EnviromentConstants.CONNECTION_STRING_SECTION);
        var connectionString = connectionSection.GetRequiredValue<string>(EnviromentConstants.DEFAULT_CONNECTION_STRING);

        Console.WriteLine(connectionString);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        using var context = new ApplicationDbContext(optionsBuilder.Options);

        Console.WriteLine("Applying migrations...");
        await context.Database.MigrateAsync();

        Console.WriteLine("Migrations applied successfully.");
    }
}
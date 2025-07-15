using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Core.Game;
using InternshipEntryTask.Core.Game.Interfaces;
using InternshipEntryTask.Core.Services;
using InternshipEntryTask.Core.Services.Interfaces;
using InternshipEntryTask.Infrastructure;
using InternshipEntryTask.Infrastructure.Repositories;
using InternshipEntryTask.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace InternshipEntryTask.Api.Extenctions;

/// <summary>
/// Класс расширений для внедрения зависимостей
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет контекст базы данных
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="configuration">Конфигурация</param>
    public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionSection = configuration.GetRequiredSection(EnviromentConstants.CONNECTION_STRING_SECTION);
        var connectionString = connectionSection.GetRequiredValue<string>(EnviromentConstants.DEFAULT_CONNECTION_STRING);

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }

    /// <summary>
    /// Добавляет сервисы
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddSingleton<IGameBoardEvaluatorFactory, GameBoardEvaluatorFactory>();
        services.AddSingleton<IETagService, ETagService>();
    }

    /// <summary>
    /// Добавляет репозщитории
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IGameRepository), typeof(GameRepository));
    }

    /// <summary>
    /// Добавляет swagger
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {

    }

    /// <summary>
    /// Добавляет логирование
    /// </summary>
    /// <param name="builder"></param>
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
    }

    /// <summary>
    /// Добавляет обработку ошибок контроллера
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов с добавленной обработкой</returns>
    public static IServiceCollection AddProblems(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = string.Join(SystemConstants.HTTP_METHOD_SEPARATOR, context.HttpContext.Request.Method, context.HttpContext.Request.Path.Value);
                context.ProblemDetails.Extensions.TryAdd(SystemConstants.REQUEST_ID_PROBLEM_DETAIL, context.HttpContext.TraceIdentifier);
                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd(SystemConstants.TRACE_ID_PROBLEM_DETAIL, activity?.Id);
            };
        });

        return services;
    }
}

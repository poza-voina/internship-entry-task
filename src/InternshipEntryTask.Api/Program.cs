using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Abstractions.Exceptions;
using InternshipEntryTask.Api;
using InternshipEntryTask.Api.Extenctions;
using InternshipEntryTask.Core.Data.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.VisualBasic;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

/// Есть какие-то опции у IConfiguration) а я все через получение секции делал) переделывать не буду.

/// <summary>
/// Точка входа в приложение
/// </summary>
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddApplicationDbContext(configuration);
        services.AddServices();
        services.AddRepositories();
        services.AddProblems();
        builder.AddSerilog();
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddMemoryCache();

        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });


        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        var app = builder.Build();
        ValidateGameSection(configuration);

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                    $"API {desc.GroupName.ToUpperInvariant()}");
            }
        });

        app.MapControllers();

        app.Run();
    }

    private static void ValidateGameSection(IConfiguration configuration)
    {
        var gameSettings = configuration.GetRequiredSection(EnviromentConstants.GAME_SECTION_NAME)
            .Get<GameSettings>()!;

        if (gameSettings.Width < 3 || gameSettings.Height < 3 || gameSettings.Width > 10000 || gameSettings.Height > 10000)
        {
            throw new EnvironmentConfigurationException(string.Format(MessagesConstants.ENV_WIDTH_HEIGHT_ERROR_MESSAGE_FORMAT, 3, 10000));
        }

        if (gameSettings.WinLength > gameSettings.Width || gameSettings.WinLength > gameSettings.Height)
        {
            throw new EnvironmentConfigurationException(string.Format(MessagesConstants.ENV_WINLENGHT_ERROR_MESSAGE_FORMAT, gameSettings.Width, gameSettings.Height));

        }
    }
}
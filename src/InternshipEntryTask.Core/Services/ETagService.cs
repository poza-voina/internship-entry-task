using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Api.Extenctions;
using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Core.Mappers;
using InternshipEntryTask.Core.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace InternshipEntryTask.Core.Services;


/// <inheritdoc/>
public class ETagService : IETagService
{
    private readonly IMemoryCache _memmoryCache;
    private readonly IConfiguration _configuration;
    private readonly TimeSpan _ttl;

    /// <summary>
    /// Создает объект <inheritdoc cref="ETagService"/>
    /// </summary>
    /// <param name="memmoryCache">Кеш</param>
    /// <param name="configuration">Конфигурация</param>
    public ETagService(IMemoryCache memmoryCache, IConfiguration configuration)
    {
        _memmoryCache = memmoryCache;
        _configuration = configuration;

        _ttl = TimeSpan.FromMinutes(
            _configuration.GetRequiredSection(EnviromentConstants.CACHE_SECTION)
            .GetRequiredValue<int>(EnviromentConstants.CACHE_TTL));
    }

    /// <inheritdoc/>
    public bool Check(long gameId, Guid accessKey, MoveRequest moveRequest, out string etag)
    {
        var (key, newValue) = GenerateKeyValue(gameId, accessKey, moveRequest.ToMoveDto());

        _memmoryCache.TryGetValue(key, out var value);
        if (newValue == value as string)
        {
            etag = value as string ?? string.Empty;
        }
        else
        {
            etag = string.Empty;
            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    public string GetETag(long gameId, Guid accessKey)
    {
        var key = string.Format(SystemConstants.ETAG_KEY_FORMAT, gameId, accessKey);

        if (!_memmoryCache.TryGetValue(key, out var value))
        {
            return string.Empty;
        }

        return value as string ?? string.Empty;
    }

    /// <inheritdoc/>
    public void SetLastMoveETag(long gameId, Guid accessKey, MoveDto moveDto)
    {
        var (key, value) = GenerateKeyValue(gameId, accessKey, moveDto);

        _memmoryCache.Set(key, value, _ttl);
    }

    private (string Key, string Value) GenerateKeyValue(long gameId, Guid accessKey, MoveDto moveDto)
    {
        var key = string.Format(SystemConstants.ETAG_KEY_FORMAT, accessKey, gameId);

        var value = string.Format(SystemConstants.ETAG_VALUE_FORMAT, moveDto.Row, moveDto.Column);

        return (key, value);
    }
}

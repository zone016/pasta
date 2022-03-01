using FastEndpoints;

using Pasta.Shared.Entities;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;

namespace Pasta.Web.Mappers;

public class ConfigurationRequestMapper : Mapper<ConfigurationRequest, ConfigurationResponse, ConfigurationEntity>
{
    public override ConfigurationEntity ToEntity(ConfigurationRequest request) => new()
    {
        Guid = request.Guid,
        CreatedAt = request.CreatedAt,
        HttpProbingPorts = request.HttpProbingPorts.Select(pNumber => new PortEntity{Number = pNumber}).ToList(),
        Headers = request.Headers.Select(h => new HeaderEntity{Name = h.Key, Value = h.Value}).ToList(),
        IsScreenshotEnable = request.IsScreenshotEnable,
        IsFaviconDownloadEnable = request.IsFaviconDownloadEnable,
        IsFaviconHashEnable = request.IsFaviconHashEnable,
        IsScreenshotHashEnable = request.IsScrenshotHashEnable,
        IsResolveNameEnable = request.IsResolveNameEnable,
        IsIgnoreCertificateErrorsEnable = request.IsIgnoreCertificateErrorsEnable,
        IsRedirectEnable = request.IsRedirectEnable,
        MaxAutomaticRedirects = request.MaxAutomaticRedirects,
        Timeout = request.Timeout,
        ScreenshotResolution = request.ScreenshotResolution,
        Title = request.Title
    };

    public override ConfigurationResponse FromEntity(ConfigurationEntity entity) => new()
    {
        Guid = entity.Guid,
        CreatedAt = entity.CreatedAt,
        HttpProbingPorts = entity.HttpProbingPorts.Select(p => p.Number).ToList(),
        Headers = entity.Headers.ToDictionary(h => h.Name, h => h.Value),
        IsScreenshotEnable = entity.IsScreenshotEnable,
        IsFaviconDownloadEnable = entity.IsFaviconDownloadEnable,
        IsFaviconHashEnable = entity.IsFaviconHashEnable,
        IsScrenshotHashEnable = entity.IsScreenshotHashEnable,
        IsResolveNameEnable = entity.IsResolveNameEnable,
        IsIgnoreCertificateErrorsEnable = entity.IsIgnoreCertificateErrorsEnable,
        IsRedirectEnable = entity.IsRedirectEnable,
        MaxAutomaticRedirects = entity.MaxAutomaticRedirects,
        Timeout = entity.Timeout,
        ScreenshotResolution = entity.ScreenshotResolution,
        Title = entity.Title
    };
}
using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Pasta.Shared;
using Pasta.Shared.Entities;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Configuration;

public class Update : Endpoint<ConfigurationRequest, ConfigurationResponse, ConfigurationRequestMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public Update(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/configurations/{guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ConfigurationRequest request, CancellationToken ct)
    {
        var element = await _dbContext.Configurations
            .Include(c => c.Headers)
            .Include(c => c.HttpProbingPorts)
            .FirstOrDefaultAsync(c => c.Guid == request.Guid, cancellationToken: ct);

        if (element is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var headers = element.Headers;
        _dbContext.Headers.RemoveRange(headers);
        await _dbContext.SaveChangesAsync(ct);

        request.Headers.ToList().ForEach(header =>
        {
            if (headers.Any(h => h.Value == header.Value || h.Name == header.Key)) return;
            
            headers.Add(new HeaderEntity
            {
                Name = header.Key,
                Value = header.Value
            });
        });

        await _dbContext.Headers.AddRangeAsync(headers, ct);
        await _dbContext.SaveChangesAsync(ct);

        var ports = element.HttpProbingPorts;
        _dbContext.Ports.RemoveRange(ports);
        await _dbContext.SaveChangesAsync(ct);

        request.HttpProbingPorts.ForEach(n =>
        {
            if (ports.Any(p => p.Number == n)) return;
            ports.Add(new PortEntity{Number = n});
        });

        await _dbContext.Ports.AddRangeAsync(ports, ct);
        await _dbContext.SaveChangesAsync(ct);

        element.Headers = headers;
        element.HttpProbingPorts = ports;
        element.IsScreenshotEnable = request.IsScreenshotEnable;
        element.IsFaviconDownloadEnable = request.IsFaviconDownloadEnable;
        element.IsFaviconHashEnable = request.IsFaviconHashEnable;
        element.IsScreenshotHashEnable = request.IsScrenshotHashEnable;
        element.ScreenshotResolution = request.ScreenshotResolution;
        element.IsResolveNameEnable = request.IsResolveNameEnable;
        element.IsIgnoreCertificateErrorsEnable = request.IsIgnoreCertificateErrorsEnable;
        element.IsRedirectEnable = request.IsRedirectEnable;
        element.MaxAutomaticRedirects = request.MaxAutomaticRedirects;
        element.Timeout = request.Timeout;
        element.Title = request.Title;

        _dbContext.Configurations.Update(element);
        await _dbContext.SaveChangesAsync(ct);

        var response = Map.FromEntity(element);
        await SendAsync(response, cancellation: ct);
    }
}
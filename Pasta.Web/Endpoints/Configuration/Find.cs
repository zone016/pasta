using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Configuration;

public class Find : Endpoint<ConfigurationRequest, ConfigurationResponse, ConfigurationMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public Find(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/configurations/{guid}");
        AllowAnonymous();
        
        DontThrowIfValidationFails();
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

        var response = Map.FromEntity(element!);
        await SendAsync(response, cancellation: ct);
    }
}
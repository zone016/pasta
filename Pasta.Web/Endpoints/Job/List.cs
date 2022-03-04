using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Pasta.Shared;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Job;

public class List : Endpoint<EmptyRequest, ConfigurationsResponse, ConfigurationRequestMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public List(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/jobs");
        AllowAnonymous();
        
        DontThrowIfValidationFails();
    }

    public override async Task HandleAsync(EmptyRequest _, CancellationToken ct)
    {
        var configurations = await _dbContext.Configurations
            .Include(c => c.Headers)
            .Include(c => c.HttpProbingPorts)
            .Select(c => Map.FromEntity(c!))
            .ToListAsync(cancellationToken: ct);

        var response = new ConfigurationsResponse {Configurations = configurations}; 

        await SendAsync(response, cancellation: ct);
    }
}
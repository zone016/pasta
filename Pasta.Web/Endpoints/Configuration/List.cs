using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Configuration;

public class List : Endpoint<ConfigurationRequest, ConfigurationResponses, ConfigurationMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public List(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/configurations");
        AllowAnonymous();
        
        DontThrowIfValidationFails();
    }

    public override async Task HandleAsync(ConfigurationRequest request, CancellationToken ct)
    {
        var configurations = await _dbContext.Configurations
            .Include(c => c.Headers)
            .Include(c => c.HttpProbingPorts)
            .Select(c => Map.FromEntity(c!))
            .ToListAsync(cancellationToken: ct);

        var response = new ConfigurationResponses {Configurations = configurations}; 

        await SendAsync(response, cancellation: ct);
    }
}
using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Configuration;

public class Delete : Endpoint<FindConfigurationRequest, ConfigurationResponse, ConfigurationRequestMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public Delete(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/configurations/{guid}");
        AllowAnonymous();        
    }

    public override async Task HandleAsync(FindConfigurationRequest request, CancellationToken ct)
    {
        var element = await _dbContext.Configurations
            .Include(c => c.Headers)
            .Include(c => c.HttpProbingPorts)
            .FirstOrDefaultAsync(c => c.Guid == Guid.Parse(request.Guid), cancellationToken: ct);
        
        if (element is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        _dbContext.Configurations.Remove(element);
        await _dbContext.SaveChangesAsync(ct);
        
        await SendNoContentAsync(ct);
    }
}
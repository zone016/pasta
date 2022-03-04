using FastEndpoints;
using FastEndpoints.Validation;

using Microsoft.EntityFrameworkCore;

using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Configuration;

public class Delete : Endpoint<FindRequest, ConfigurationResponse, ConfigurationRequestMapper>
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

    public override async Task HandleAsync(FindRequest request, CancellationToken ct)
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
        
        // If there is a running job using this configuration
        var isConfigurationInUse = await _dbContext.Jobs
            .Include(j => j.Configuration)
            .AnyAsync(j => j.IsRunning && j.Configuration.Guid == element.Guid, cancellationToken: ct);

        if (isConfigurationInUse)
        {
            ValidationFailures.Add(new ValidationFailure(nameof(request.Guid),
                "Configuration is already utilized by a running job!"));
            
            await SendErrorsAsync(ct);
            return;
        }
        
        _dbContext.Configurations.Remove(element);
        await _dbContext.SaveChangesAsync(ct);
        
        await SendNoContentAsync(ct);
    }
}
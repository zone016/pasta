using FastEndpoints;
using FastEndpoints.Validation;
using Microsoft.EntityFrameworkCore;

using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Job;

public class Create : Endpoint<JobRequest, JobResponse, JobRequestMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public Create(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/jobs");
        AllowAnonymous();
    }

    public override async Task HandleAsync(JobRequest request, CancellationToken ct)
    {
        var configuration = await _dbContext.Configurations
            .Include(c => c.Headers)
            .Include(c => c.HttpProbingPorts)
            .FirstOrDefaultAsync(c => c.Guid == Guid.Parse(request.ConfigurationGuid), cancellationToken: ct);
        
        if (configuration is null)
        {
            ValidationFailures.Add(new ValidationFailure(nameof(request.ConfigurationGuid),
                "Given configuration does not exist!"));
            await SendErrorsAsync(ct);
            
            return;
        }
        
        var element = Map.ToEntity(request);
        element.Configuration = configuration;

        var entry = await _dbContext.AddAsync(element, ct);
        await _dbContext.SaveChangesAsync(ct);

        var response = Map.FromEntity(entry.Entity);
        await SendCreatedAtAsync("/jobs/", response.Guid, response, ct);
    }
}
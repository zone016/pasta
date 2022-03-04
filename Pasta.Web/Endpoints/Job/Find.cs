using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Job;

public class Find : Endpoint<FindRequest, JobResponse, JobRequestMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public Find(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/jobs/{guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(FindRequest request, CancellationToken ct)
    {
        var element = await _dbContext.Jobs
            .Include(j => j.Webhooks)
            .FirstOrDefaultAsync(j => j.Guid == Guid.Parse(request.Guid), cancellationToken: ct);

        if (element is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var response = Map.FromEntity(element);
        await SendAsync(response, cancellation: ct);
    }
}
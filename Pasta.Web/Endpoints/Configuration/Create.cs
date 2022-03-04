using FastEndpoints;

using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Configuration;

public class Create : Endpoint<ConfigurationRequest, ConfigurationResponse, ConfigurationRequestMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public Create(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/configurations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ConfigurationRequest request, CancellationToken ct)
    {
        var entity = Map.ToEntity(request);
        var entry = await _dbContext.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);

        var response = Map.FromEntity(entry.Entity);
        await SendCreatedAtAsync("/configurations/", response.Guid, response, ct);
    }
}
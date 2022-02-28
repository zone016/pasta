﻿using FastEndpoints;

using Pasta.Shared;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;
using Pasta.Web.Mappers;

namespace Pasta.Web.Endpoints.Configuration;

public class Save : Endpoint<ConfigurationRequest, ConfigurationResponse, ConfigurationMapper>
{
    private readonly ApplicationDbContext _dbContext;

    public Save(ApplicationDbContext dbContext)
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
        var element = Map.ToEntity(request);

        var entry = await _dbContext.AddAsync(element, ct);
        await _dbContext.SaveChangesAsync(ct);

        var response = Map.FromEntity(entry.Entity);
        await SendAsync(response, cancellation: ct);
    }
}
using FastEndpoints;

using Pasta.Shared.Entities;
using Pasta.Shared.Enums;
using Pasta.Shared.Requests;
using Pasta.Shared.Responses;

namespace Pasta.Web.Mappers;

public class JobRequestMapper : Mapper<JobRequest, JobResponse, JobEntity>
{
    public override JobEntity ToEntity(JobRequest request) => new()
    {
        Title = request.Title,
        Target = request.Target,
        Webhooks = request.Webhooks.Select(w => new WebhookEntity
        {
            CreatedAt = DateTime.Now,
            Type = Enum.Parse<WebhookType>(w.Type),
            Address = w.Address
        }).ToList(),
        Priority = Enum.Parse<Priority>(request.Priority),
        Configuration = new ConfigurationEntity{Guid = Guid.Parse(request.ConfigurationGuid)}
    };

    public override JobResponse FromEntity(JobEntity entity) => new()
    {
        Guid = entity.Guid.ToString(),
        Title = entity.Title,
        Target = entity.Target,
        Priority = Enum.GetName(entity.Priority)!,
        Status = Enum.GetName(entity.Status)!,
        Phase = Enum.GetName(entity.Phase)!,
        Webhooks = entity.Webhooks.Select(w => new WebhookResponse
        {
            Type = w.Type,
            Address = w.Address
        }).ToList(),
        IsRunning = entity.IsRunning,
        ConfigurationGuid = entity.Configuration.Guid.ToString()
    };
}
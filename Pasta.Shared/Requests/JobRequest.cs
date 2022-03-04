namespace Pasta.Shared.Requests;

public class JobRequest
{
    public string Title { get; init; } = default!;
    public string Target { get; init; } = default!;
    public string ConfigurationGuid { get; init; } = default!;
    public List<WebhookRequest> Webhooks { get; init; } = new();
    public string Priority { get; init; } = Enum.GetName(Enums.Priority.Normal)!;
}
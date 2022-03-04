namespace Pasta.Shared.Responses;

public record JobResponse
{
    public string Guid { get; init; } = default!;
    public string Title { get; init; } = default!;
    public string Target { get; init; } = default!;
    public string Priority { get; init; } = default!;
    public string Status { get; init; } = default!;
    public string Phase { get; init; } = default!;
    public List<WebhookResponse> Webhooks { get; init; } = new();
    public List<string> Stdout { get; init; } = new();
    public List<string> Stderr { get; init; } = new();
    public bool IsRunning { get; set; }
    public string ConfigurationGuid { get; init; } = default!;
}
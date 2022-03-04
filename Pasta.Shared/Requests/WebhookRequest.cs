namespace Pasta.Shared.Requests;

public record WebhookRequest
{
    public string Type { get; init; } = default!;
    public string Address { get; init; } = default!;
}
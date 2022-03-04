using Pasta.Shared.Enums;

namespace Pasta.Shared.Responses;

public record WebhookResponse
{
    public WebhookType Type { get; init; } = default!;
    public string Address { get; init; } = default!;
}
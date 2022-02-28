namespace Pasta.Shared.Responses;

public record ConfigurationResponses
{
    public IEnumerable<ConfigurationResponse> Configurations { get; init; } = default!;
}
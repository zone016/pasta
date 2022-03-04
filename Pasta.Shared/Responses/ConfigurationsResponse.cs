namespace Pasta.Shared.Responses;

public record ConfigurationsResponse
{
    public IEnumerable<ConfigurationResponse> Configurations { get; init; } = default!;
}
namespace Pasta.Shared.Responses;

public class ConfigurationResponse
{
    public Guid Guid { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<int> HttpProbingPorts { get; init; } = default!;
    public Dictionary<string, string> Headers { get; init; } = default!;
    public bool IsScreenshotEnable { get; init; }
    public bool IsFaviconDownloadEnable { get; init; }
    public bool IsFaviconHashEnable { get; init; }
    public bool IsScrenshotHashEnable { get; init; }
    public bool IsResolveNameEnable { get; init; }
    public bool IsIgnoreCertificateErrorsEnable { get; init; }
    public bool IsRedirectEnable { get; init; }
    public int MaxAutomaticRedirects { get; init; }
    public int Timeout { get; init; }
    public string ScreenshotResolution { get; init; } = default!;
    public string Title { get; init; } = default!;
}
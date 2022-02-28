namespace Pasta.Shared.Requests;

public record ConfigurationRequest
{
    public Guid Guid { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<int> HttpProbingPorts { get; init; } = new();
    public Dictionary<string, string> Headers { get; init; } = new();
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
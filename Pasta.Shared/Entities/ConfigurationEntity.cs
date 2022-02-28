using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pasta.Shared.Entities;

[Table("CONFIGURATIONS")]
public class ConfigurationEntity
{
    [Key] public Guid Guid { get; init; } = Guid.NewGuid();

    [Required] public DateTime CreatedAt { get; init; } = DateTime.Now;

    [Required] public List<Port> HttpProbingPorts { get; init; } = new();
    [Required] public List<Header> Headers { get; init; } = new();

    [Required] public bool IsScreenshotEnable { get; init; }
    [Required] public bool IsFaviconDownloadEnable { get; init; }
    [Required] public bool IsFaviconHashEnable { get; init; }
    [Required] public bool IsScrenshotHashEnable { get; init; }
    [Required] public bool IsResolveNameEnable { get; init; }
    [Required] public bool IsIgnoreCertificateErrorsEnable { get; init; }
    [Required] public bool IsRedirectEnable { get; init; }

    [Required] public int MaxAutomaticRedirects { get; init; }
    [Required] public int Timeout { get; init; }

    [Required, MaxLength(4096)] public string ScreenshotResolution { get; init; } = default!;
    [Required, MaxLength(256)] public string Title { get; init; } = default!;
}
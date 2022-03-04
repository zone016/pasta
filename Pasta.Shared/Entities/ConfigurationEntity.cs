using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pasta.Shared.Entities;

[Table("CONFIGURATIONS")]
public record class ConfigurationEntity
{
    [Key] public Guid Guid { get; set; } = Guid.NewGuid();

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public List<PortEntity> HttpProbingPorts { get; set; } = new();
    [Required] public List<HeaderEntity> Headers { get; set; } = new();

    [Required] public bool IsScreenshotEnable { get; set; }
    [Required] public bool IsFaviconDownloadEnable { get; set; }
    [Required] public bool IsFaviconHashEnable { get; set; }
    [Required] public bool IsScreenshotHashEnable { get; set; }
    [Required] public bool IsResolveNameEnable { get; set; }
    [Required] public bool IsIgnoreCertificateErrorsEnable { get; set; }
    [Required] public bool IsRedirectEnable { get; set; }

    [Required] public int MaxAutomaticRedirects { get; set; }
    [Required] public int Timeout { get; set; }

    [Required, MaxLength(128)] public string ScreenshotResolution { get; set; } = default!;
    [Required, MaxLength(256)] public string Title { get; set; } = default!;
}
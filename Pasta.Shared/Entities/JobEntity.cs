using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Pasta.Shared.Enums;

namespace Pasta.Shared.Entities;

[Table("JOBS")]
public record JobEntity
{
    [Key] public Guid Guid { get; init; } = Guid.NewGuid();
    [Required] public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    [Required, MaxLength(256)] public string Title { get; set; } = default!;
    [Required, MaxLength(2048)] public string Target { get; set; } = default!;

    [Required] public bool IsRunning { get; set; } = false;
    [Required] public Priority Priority { get; set; }
    [Required] public Status Status { get; set; }
    [Required] public Phase Phase { get; set; }
    [Required] public ConfigurationEntity Configuration { get; set; } = default!;

    public List<WebhookEntity> Webhooks { get; set; } = new();
}
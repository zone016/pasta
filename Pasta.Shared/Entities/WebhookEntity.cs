using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Pasta.Shared.Enums;

namespace Pasta.Shared.Entities;

[Table("WEBHOOKS")]
public record WebhookEntity
{
    [Key] public Guid Guid { get; init; } = Guid.NewGuid();
    [Required] public DateTime CreatedAt { get; init; } = DateTime.Now;
    [Required] public WebhookType Type { get; init; } = default!;
    [Required, MaxLength(4096)] public string Address { get; init; } = default!;
}
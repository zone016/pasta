using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pasta.Shared.Entities;

[Table("HEADERS")]
public record HeaderEntity
{
    [Key] public Guid Guid { get; init; } = Guid.NewGuid();
    [Required, MaxLength(1024)] public string Name { get; init; } = default!;
    [Required, MaxLength(4096)] public string Value { get; init; } = default!;
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pasta.Shared.Entities;

[Table("PORTS")]
public class Port
{
    [Key] public Guid Guid { get; init; }
    [Required] public int Number { get; init; }
}
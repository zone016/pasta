﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pasta.Shared.Entities;

[Table("REPORTS")]
public class Report
{
    [Key] public Guid Guid { get; init; } = Guid.NewGuid();
    [Required] public DateTime CreatedAt { get; init; } = DateTime.Now;
}
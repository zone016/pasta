using Pasta.Shared.Entities;

namespace Pasta.Shared.Enums;

/// <summary>
/// In what phase is a job.
/// </summary>
public enum Priority
{
    /// <summary>
    /// If the <see cref="Job"/> is a low priority task.
    /// </summary>
    Low = 0x00,
    
    /// <summary>
    /// If the <see cref="Job"/> is a normal priority task.
    /// </summary>
    Normal = 0x01,
    
    /// <summary>
    /// If the <see cref="Job"/> is a high priority task.
    /// </summary>
    High = 0x02
}
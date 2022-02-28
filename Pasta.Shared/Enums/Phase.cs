namespace Pasta.Shared.Enums;

/// <summary>
/// In what phase is a job.
/// </summary>
public enum Phase
{
    /// <summary>
    /// If the <see cref="Phase"/> is on enumeration phase.
    /// </summary>
    Enumeration = 0x00,
    
    /// <summary>
    /// If the <see cref="Phase"/> is on probing phase.
    /// </summary>
    Probing = 0x01
}
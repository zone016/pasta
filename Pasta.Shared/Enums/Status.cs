namespace Pasta.Shared.Enums;

/// <summary>
/// The health of the job.
/// </summary>
public enum Status
{
    /// <summary>
    /// If worked as expected.
    /// </summary>
    Sucessful = 0x00,
    
    /// <summary>
    /// If worked, but with errors.
    /// </summary>
    WithErrors = 0x01,
    
    /// <summary>
    /// If not worked.
    /// </summary>
    Failure = 0x02
}
using Pasta.Shared.Entities;

namespace Pasta.Shared.Enums;

/// <summary>
/// Type of the <see cref="Webhook"/>.
/// </summary>
public enum WebhookType
{
    /// <summary>
    /// If the webhook is made to Google Workspace.
    /// </summary>
    GoogleChat = 0x00,
    
    /// <summary>
    /// If the webhook is made to Slack.
    /// </summary>
    Slack = 0x01
}
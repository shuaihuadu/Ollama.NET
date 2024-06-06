namespace Ollama.Core.Models.Base;

/// <summary>
/// The completion response base.
/// </summary>
public abstract class CompletionResponseBase
{
    /// <summary>
    /// The model name.
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// Gets the timestamp associated with generation activity for this completions response.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets the done status.
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; set; }
}

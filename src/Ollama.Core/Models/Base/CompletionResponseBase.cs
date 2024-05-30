namespace Ollama.Core.Models.Base;

public abstract class CompletionResponseBase
{
    /// <summary>
    /// The model name.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; protected set; } = null!;

    /// <summary>
    /// Gets the timestamp associated with generation activity for this completions response.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; protected set; }

    /// <summary>
    /// Gets the done status.
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; protected set; }
}

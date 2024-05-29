namespace Ollama.Core.Models;

public partial class StreamingCompletionUpdate
{
    [JsonConstructor]
    internal StreamingCompletionUpdate(string model, DateTimeOffset createdAt, string response, bool done)
    {
        this.Model = model;
        this.CreatedAt = createdAt;
        this.Response = response;
        this.Done = done;
    }

    /// <summary>
    /// The model name
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; }

    /// <summary>
    /// Gets the timestamp associated with generation activity for this completions response
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; }

    /// <summary>
    /// Gets the done status.
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; }
}

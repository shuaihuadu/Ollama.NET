namespace Ollama.Core.Models;

public class LoadModelResponse : CompletionResponseBase
{
    [JsonConstructor]
    internal LoadModelResponse(
        string model,
        DateTimeOffset createdAt,
        string response,
        bool done)
    {
        this.Model = model;
        this.CreatedAt = createdAt;
        this.Response = response;
        this.Done = done;
    }

    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; }
}

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
        Model = model;
        CreatedAt = createdAt;
        Response = response;
        Done = done;
    }

    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; }
}

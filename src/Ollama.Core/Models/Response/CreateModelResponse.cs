namespace Ollama.Core.Models;

public class CreateModelResponse
{
    [JsonConstructor]
    internal CreateModelResponse(string status)
    {
        this.Status = status;
    }

    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; }
}

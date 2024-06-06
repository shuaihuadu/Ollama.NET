namespace Ollama.Core.Models;

/// <summary>
/// The create model response.
/// </summary>
public class CreateModelResponse
{
    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; }
}

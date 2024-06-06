namespace Ollama.Core.Models;

/// <summary>
/// The list running model response.
/// </summary>
public sealed class ListRunningModelResponse
{
    /// <summary>
    /// The models.
    /// </summary>
    [JsonPropertyName("models")]
    public IList<ListModelItem> Models { get; set; } = [];
}
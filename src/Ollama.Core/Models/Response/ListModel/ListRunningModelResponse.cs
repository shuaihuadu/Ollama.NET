namespace Ollama.Core.Models;

public sealed class ListRunningModelResponse
{
    /// <summary>
    /// The models.
    /// </summary>
    [JsonPropertyName("models")]
    public IList<ListModelItem> Models { get; set; } = [];
}
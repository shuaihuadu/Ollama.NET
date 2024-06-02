namespace Ollama.Core.Models;

public sealed class ListModelResponse
{
    /// <summary>
    /// The models.
    /// </summary>
    [JsonPropertyName("models")]
    public IList<ListModelItem> Models { get; set; } = [];
}
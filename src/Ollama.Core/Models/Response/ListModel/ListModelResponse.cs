namespace Ollama.Core.Models;

/// <summary>
/// The list mode response.
/// </summary>
public sealed class ListModelResponse
{
    /// <summary>
    /// The models.
    /// </summary>
    [JsonPropertyName("models")]
    public IList<ListModelItem> Models { get; set; } = [];
}
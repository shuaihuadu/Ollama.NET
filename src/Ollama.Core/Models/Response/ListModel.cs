namespace Ollama.Core.Models;

/// <summary>
/// 
/// </summary>
public sealed class ListModel
{
    [JsonPropertyName("models")]
    public IList<ListModelItem> Models { get; set; } = [];
}
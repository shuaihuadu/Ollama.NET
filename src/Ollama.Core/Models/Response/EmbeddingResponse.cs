namespace Ollama.Core.Models;

/// <summary>
/// The embedding response.
/// </summary>
public sealed class EmbeddingResponse
{
    /// <summary>
    /// The generated embeddings.
    /// </summary>
    [JsonPropertyName("embedding")]
    public ReadOnlyMemory<float> Embedding { get; set; }
}
namespace Ollama.Core.Models;

public sealed class EmbeddingResponse
{
    /// <summary>
    /// The generated embeddings.
    /// </summary>
    [JsonPropertyName("embedding")]
    public ReadOnlyMemory<float>? Embedding { get; set; }
}
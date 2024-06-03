namespace Ollama.Core.Models;

/// <inheritdoc />
internal class GenerateCompletionRequest : GenerateCompletionRequestBase
{
    /// <summary>
    /// If false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream => false;
}
namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion"/>
/// </summary>
internal sealed class GenerateCompletionStreamingRequest : GenerateCompletionRequestBase
{
    /// <inheritdoc />
    public GenerateCompletionStreamingRequest() { }

    /// <inheritdoc />
    [SetsRequiredMembers]
    public GenerateCompletionStreamingRequest(GenerateCompletionOptions options) : base(options) { }

    /// <inheritdoc />
    [SetsRequiredMembers]
    public GenerateCompletionStreamingRequest(string model, string prompt) : base(model, prompt) { }

    /// <summary>
    /// If false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream => true;
}
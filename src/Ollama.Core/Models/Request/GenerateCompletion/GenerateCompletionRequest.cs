namespace Ollama.Core.Models;

/// <inheritdoc />
internal sealed class GenerateCompletionRequest : GenerateCompletionRequestBase
{
    public GenerateCompletionRequest() { }

    [SetsRequiredMembers]
    public GenerateCompletionRequest(string model, string prompt) : base(model, prompt) { }

    [SetsRequiredMembers]
    public GenerateCompletionRequest(GenerateCompletionOptions options) : base(options) { }

    /// <summary>
    /// If false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream => false;
}
namespace Ollama.Core.Models;

/// <inheritdoc />
internal sealed class GenerateCompletionRequest : GenerateCompletionRequestBase
{

    /// <inheritdoc />
    [SetsRequiredMembers]
    public GenerateCompletionRequest(string model, string prompt, bool stream) : base(model, prompt, stream) { }

    /// <inheritdoc />
    [SetsRequiredMembers]
    public GenerateCompletionRequest(GenerateCompletionOptions options) : base(options) { }
}
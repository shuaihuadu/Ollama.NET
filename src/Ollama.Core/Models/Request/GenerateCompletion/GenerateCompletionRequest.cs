namespace Ollama.Core.Models;

/// <inheritdoc />
internal sealed class GenerateCompletionRequest : GenerateCompletionRequestBase
{

    /// <inheritdoc />
    public GenerateCompletionRequest(string model, string prompt, bool stream) : base(model, prompt, stream) { }

    /// <inheritdoc />
    public GenerateCompletionRequest(GenerateCompletionOptions options) : base(options) { }
}
namespace Ollama.Core.Models;

/// <inheritdoc />
internal sealed class GenerateCompletionRequest : GenerateCompletionRequestBase
{
    public GenerateCompletionRequest() { }

    [SetsRequiredMembers]
    public GenerateCompletionRequest(string model, string prompt, bool stream) : base(model, prompt, stream) { }

    [SetsRequiredMembers]
    public GenerateCompletionRequest(GenerateCompletionOptions options) : base(options) { }
}
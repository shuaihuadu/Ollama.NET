namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion"/>
/// </summary>
internal sealed class ChatCompletionRequest : ChatCompletionRequestBase
{
    /// <inheritdoc />
    public ChatCompletionRequest() { }

    /// <inheritdoc />
    [SetsRequiredMembers]
    public ChatCompletionRequest(string model, ChatMessageHistory messages, bool stream) : base(model, messages, stream) { }

    /// <inheritdoc />
    [SetsRequiredMembers]
    public ChatCompletionRequest(ChatCompletionOptions options) : base(options) { }
}
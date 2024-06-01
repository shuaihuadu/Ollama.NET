namespace Ollama.Core.Models;

/// <summary>
/// The request for preload the model using the chat endpoint.
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/faq.md"/>
/// </summary>
internal sealed class LoadModelUseChatCompletionRequest : LoadModelRequest
{
    /// <inheritdoc/>
    public override HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/chat", this);
    }
}
namespace Ollama.Core.Models;

/// <summary>
/// The request for preload the model using the chat endpoint. <br />
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/faq.md"/>
/// </summary>
internal sealed class LoadModelUseChatCompletionRequest : LoadModelRequestBase
{
    /// <inheritdoc/>
    public override HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/chat", this);
    }
}
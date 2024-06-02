namespace Ollama.Core.Models;

/// <summary>
/// The request for preload the model using the generate endpoint.
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion"/>
/// </summary>
internal sealed class LoadModelUseGenerateCompletionRequest : LoadModelRequestBase
{
    /// <inheritdoc/>
    public override HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/generate", this);
    }
}
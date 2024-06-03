namespace Ollama.Core.Models;

/// <inheritdoc />
internal abstract class GenerateCompletionRequestBase : GenerateCompletionOptions
{
    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/generate", this);
    }
}
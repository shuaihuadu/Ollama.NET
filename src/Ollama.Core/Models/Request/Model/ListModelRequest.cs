namespace Ollama.Core.Models;

/// <summary>
/// The request for list models. <br />
/// <a href="https://github.com/ollama/ollama/blob/main/docs/api.md#list-local-models" />
/// <code>ollama list</code>
/// </summary>
internal sealed class ListModelRequest
{
    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreateGetRequest("api/tags");
    }
}
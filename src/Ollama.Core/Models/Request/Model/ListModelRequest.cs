namespace Ollama.Core.Models;

/// <summary>
/// The request for list models. <br />
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#list-local-models"/>
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
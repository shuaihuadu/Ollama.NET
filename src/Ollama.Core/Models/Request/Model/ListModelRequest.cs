namespace Ollama.Core.Models;

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
namespace Ollama.Core.Models;

/// <summary>
/// The request for list running models. <br />
/// <code>ollama ps</code>
/// </summary>
internal sealed class ListRunningModelRequest
{
    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreateGetRequest("api/ps");
    }
}
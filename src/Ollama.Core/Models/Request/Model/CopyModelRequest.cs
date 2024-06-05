namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#copy-a-model"/>
/// </summary>
internal sealed class CopyModelRequest
{
    /// <summary>
    /// The source model name.
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; }

    /// <summary>
    /// The model name of destination.
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/copy", this);
    }
}
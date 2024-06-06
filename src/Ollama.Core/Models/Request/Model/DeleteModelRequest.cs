namespace Ollama.Core.Models;

/// <summary>
/// The request for delete model. <br />
/// https://github.com/ollama/ollama/blob/main/docs/api.md#delete-a-model
/// </summary>
internal sealed class DeleteModelRequest
{
    /// <summary>
    /// The model name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreateDeleteRequest("api/delete", this);
    }
}
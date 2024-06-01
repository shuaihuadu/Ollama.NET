namespace Ollama.Core.Models;

/// <summary>
/// The request for preload the model using the generate endpoint.
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion"/>
/// </summary>
public class LoadModelUseGenerateCompletionRequest
{
    /// <summary>
    /// The model name
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/generate", this);
    }
}
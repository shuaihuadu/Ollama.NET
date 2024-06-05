namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model"/>
/// </summary>
internal abstract class CreateModelRequestBase
{
    /// <summary>
    /// Name of the model to create.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Contents of the Modelfile.
    /// <para>
    /// <see cref="https://github.com/ollama/ollama/blob/main/docs/modelfile.md"/>
    /// </para>
    /// </summary>
    [JsonPropertyName("modelfile")]
    public string ModelFileContent { get; set; }

    /// <summary>
    /// Path to the Modelfile
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/create", this);
    }
}
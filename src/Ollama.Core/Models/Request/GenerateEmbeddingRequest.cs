namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-embeddings"/>
/// </summary>
internal sealed class GenerateEmbeddingRequest
{
    /// <summary>
    /// Name of model to generate embeddings from.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; }
    /// <summary>
    /// Text to generate embeddings for.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; }

    /// <summary>
    /// Controls how long the model will stay loaded into memory following the request (default: 5m)
    /// <para>The keep_alive parameter can be set to:</para>
    /// <list type="bullet"> a duration string (such as "10m" or "24h")
    /// <item> a number in seconds(such as 3600) </item> 
    /// <item> any negative number which will keep the model loaded in memory(e.g. -1 or "-1m") </item> 
    /// <item> '0' which will unload the model immediately after generating a response </item> 
    /// </list> 
    /// </summary>
    [JsonPropertyName("keep_alive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? KeepAlive { get; set; }

    /// <summary>
    /// Additional model parameters listed in the documentation for the Modelfile such as temperature
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParameterOptions? Options { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/embeddings", this);
    }
}
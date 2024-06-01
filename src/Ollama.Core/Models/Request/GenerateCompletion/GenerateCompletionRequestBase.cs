namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion"/>
/// </summary>
public abstract class GenerateCompletionRequestBase
{
    /// <summary>
    /// The model name
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }
    /// <summary>
    /// The prompt to generate a response for
    /// </summary>
    [JsonPropertyName("prompt")]
    public required string Prompt { get; set; }

    /// <summary>
    /// A list of base64-encoded images(for multimodal models such as llava)
    /// </summary>
    [JsonPropertyName("images")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Images { get; set; }

    /// <summary>
    /// The format to return a response in.
    /// Currently the only accepted value is json.
    /// </summary>
    [JsonPropertyName("format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Format { get; set; }

    /// <summary>
    /// System message to (overrides what is defined in the Modelfile)
    /// </summary>
    [JsonPropertyName("system")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? System { get; set; }

    /// <summary>
    /// The prompt template to use(overrides what is defined in the Modelfile)
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// The context parameter returned from a previous request to /generate, this can be used to keep a short conversational memory
    /// </summary>
    [JsonPropertyName("context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Context { get; set; }

    /// <summary>
    /// If true no formatting will be applied to the prompt.You may choose to use the raw parameter if you are specifying a full templated prompt in your request to the API
    /// </summary>
    [JsonPropertyName("raw")]
    public bool Raw { get; set; }

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
    public CompletionOptions? Options { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/generate", this);
    }
}
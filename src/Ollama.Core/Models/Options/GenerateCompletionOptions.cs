namespace Ollama.Core.Models;

/// <summary>
/// https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion
/// </summary>
public class GenerateCompletionOptions
{
    /// <summary>
    /// The model name.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = null!;
    /// <summary>
    /// The prompt to generate a response for.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = null!;

    /// <summary>
    /// A list of base64-encoded images(for multimodal models such as llava).
    /// </summary>
    [JsonPropertyName("images")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Images { get; set; }

    /// <summary>
    /// The format to return a response in.
    /// Currently the only accepted value is json.
    /// When format is set to json, the output will always be a well-formed JSON object. It's important to also instruct the model to respond in JSON.
    /// </summary>
    [JsonPropertyName("format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Format { get; set; }

    /// <summary>
    /// Additional model parameters listed in the documentation for the Modelfile such as temperature.
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParameterOptions? Options { get; set; }

    /// <summary>
    /// System message to (overrides what is defined in the Modelfile).
    /// </summary>
    [JsonPropertyName("system")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? System { get; set; }

    /// <summary>
    /// The prompt template to use(overrides what is defined in the Modelfile).
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// The context parameter returned from a previous request to /generate, this can be used to keep a short conversational memory.
    /// </summary>
    [JsonPropertyName("context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long[]? Context { get; set; }

    /// <summary>
    /// If false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream { get; protected set; }

    /// <summary>
    /// If true no formatting will be applied to the prompt.You may choose to use the raw parameter if you are specifying a full templated prompt in your request to the API.<br />
    /// In some cases, you may wish to bypass the templating system and provide a full prompt. In this case, you can use the raw parameter to disable templating. Also note that raw mode will not return a context.
    /// </summary>
    [JsonPropertyName("raw")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Raw { get; set; }

    /// <summary>
    /// Controls how long the model will stay loaded into memory following the request (default: 5m).
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
}
namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion"/>
/// </summary>
public class ChatCompletionOptions
{
    /// <summary>
    /// The model name.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The messages of the chat, this can be used to keep a chat memory.
    /// </summary>
    [JsonPropertyName("messages")]
    public required ChatMessageHistory Messages { get; set; }

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
    /// If false the response will be returned as a single response object, rather than a stream of objects.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream { get; protected set; }

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
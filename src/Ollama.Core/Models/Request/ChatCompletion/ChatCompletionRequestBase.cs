namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion"/>
/// </summary>
public abstract class ChatCompletionRequestBase
{
    /// <summary>
    /// The model name
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The messages of the chat, this can be used to keep a chat memory
    /// </summary>
    [JsonPropertyName("messages")]
    public required ChatMessageHistory Messages { get; set; }

    /// <summary>
    /// The format to return a response in.
    /// Currently the only accepted value is json.
    /// </summary>
    [JsonPropertyName("format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Format { get; set; }

    /// <summary>
    /// Controls how long the model will stay loaded into memory following the request(default: 5m)
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
        return HttpRequest.CreatePostRequest("/api/chat", this);
    }
}
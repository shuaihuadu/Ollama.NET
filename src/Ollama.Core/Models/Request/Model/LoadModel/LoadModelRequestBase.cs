namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/faq.md"/>
/// </summary>
internal abstract class LoadModelRequestBase
{
    /// <summary>
    /// The model name
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// By default models are kept in memory for 5 minutes before being unloaded.
    /// <list type="bullet">
    /// <item>The keep_alive parameter can be set to:</item>
    /// <item>a duration string (such as "10m" or "24h")</item>
    /// <item>a number in seconds(such as 3600)</item>
    /// <item>any negative number which will keep the model loaded in memory(e.g. -1 or "-1m")</item>
    /// <item>'0' which will unload the model immediately after generating a response</item>
    /// </list>
    /// </summary>
    [JsonPropertyName("keep_alive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? KeepAlive { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public abstract HttpRequestMessage ToHttpRequestMessage();
}
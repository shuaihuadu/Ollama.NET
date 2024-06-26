﻿namespace Ollama.Core.Models;

/// <summary>
/// <a href="https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model"/>
/// </summary>
internal abstract class PullModelRequestBase
{
    /// <summary>
    /// Name of the model to pull.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Allow insecure connections to the library. 
    /// Only use this if you are pulling from your own library during development.
    /// </summary>
    [JsonPropertyName("insecure")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Insecure { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("/api/pull", this);
    }
}
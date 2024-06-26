﻿namespace Ollama.Core.Models;

/// <summary>
/// The request for show model informration. <br />
/// <a href="https://github.com/ollama/ollama/blob/main/docs/api.md#show-model-information"/>
/// </summary>
internal sealed class ShowModelRequest
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
        return HttpRequest.CreatePostRequest("api/show", this);
    }
}
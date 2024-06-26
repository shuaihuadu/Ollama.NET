﻿namespace Ollama.Core.Models;

/// <summary>
/// The request for preload the model using the generate endpoint. <br />
/// <a href="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion "/><br />
/// <a href="https://github.com/ollama/ollama/blob/main/docs/faq.md" />
/// </summary>
internal sealed class LoadModelUseGenerateCompletionRequest : LoadModelRequestBase
{
    /// <inheritdoc/>
    public override HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/generate", this);
    }
}
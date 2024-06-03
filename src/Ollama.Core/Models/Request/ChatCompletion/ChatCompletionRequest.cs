namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion"/>
/// </summary>
internal sealed class ChatCompletionRequest : ChatCompletionRequestBase
{
    //public ChatCompletionRequest(ChatCompletionOptions options)
    //{
    //    this.Model = options.Model;
    //    this.Messages = options.Messages;
    //    this.Format = options.Format;
    //    this.KeepAlive = options.KeepAlive;
    //    this.Options = options.Options;
    //}

    /// <summary>
    /// If false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream => false;
}
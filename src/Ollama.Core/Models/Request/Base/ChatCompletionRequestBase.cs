namespace Ollama.Core.Models;

/// <inheritdoc />
internal abstract class ChatCompletionRequestBase : ChatCompletionOptions
{
    /// <summary>  
    /// Initializes a new instance of the <see cref="ChatCompletionRequestBase"/> class with the specified model and messages.  
    /// </summary>  
    /// <param name="model">The model name.</param>  
    /// <param name="messages">The messages of the chat, this can be used to keep a chat memory.</param>
    /// <param name="stream">If false the response will be returned as a single response object, rather than a stream of objects</param>
    [SetsRequiredMembers]
    public ChatCompletionRequestBase(string model, ChatMessageHistory messages, bool stream)
    {
        this.Model = model;
        this.Messages = messages;
        this.Stream = stream;
    }

    /// <summary>  
    /// Initializes a new instance of the <see cref="ChatCompletionRequestBase"/> class with the specified options.  
    /// </summary>
    /// <param name="options">The options to use for chat completions, including model, messages, and additional settings.</param>
    [SetsRequiredMembers]
    public ChatCompletionRequestBase(ChatCompletionOptions options)
    {
        Argument.AssertNotNull(options, nameof(options));

        this.Model = options.Model;
        this.Messages = options.Messages;
        this.Format = options.Format;
        this.Options = options.Options;
        this.Stream = options.Stream;
        this.KeepAlive = options.KeepAlive;
    }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("/api/chat", this);
    }
}
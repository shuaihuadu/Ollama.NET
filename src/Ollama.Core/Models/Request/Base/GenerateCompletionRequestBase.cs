namespace Ollama.Core.Models;

/// <inheritdoc />
internal abstract class GenerateCompletionRequestBase : GenerateCompletionOptions
{
    /// <summary>  
    /// Initializes a new instance of the <see cref="GenerateCompletionRequestBase"/> class with the specified model and prompt.  
    /// </summary>  
    /// <param name="model">The model name.</param>  
    /// <param name="prompt"> The prompt to generate a response for.</param>
    /// <param name="stream">If false the response will be returned as a single response object, rather than a stream of objects</param>
    public GenerateCompletionRequestBase(string model, string prompt, bool stream)
    {
        this.Model = model;
        this.Prompt = prompt;
        this.Stream = stream;
    }

    /// <summary>  
    /// Initializes a new instance of the <see cref="GenerateCompletionRequestBase"/> class with the specified options.  
    /// </summary>
    /// <param name="options">The options to use for generating completions, including model, prompt, and additional settings.</param>
    public GenerateCompletionRequestBase(GenerateCompletionOptions options)
    {
        Argument.AssertNotNull(options, nameof(options));

        this.Model = options.Model;
        this.Prompt = options.Prompt;
        this.Images = options.Images;
        this.Format = options.Format;
        this.Options = options.Options;
        this.System = options.System;
        this.Template = options.Template;
        this.Context = options.Context;
        this.Stream = options.Stream;
        this.Raw = options.Raw;
        this.KeepAlive = options.KeepAlive;
    }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest("api/generate", this);
    }
}
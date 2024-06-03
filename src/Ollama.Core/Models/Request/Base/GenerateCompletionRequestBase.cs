namespace Ollama.Core.Models;

/// <inheritdoc />
internal abstract class GenerateCompletionRequestBase : GenerateCompletionOptions
{
    /// <summary>  
    /// Initializes a new instance of the <see cref="GenerateCompletionRequestBase"/> class.  
    /// </summary>  
    public GenerateCompletionRequestBase() { }

    /// <summary>  
    /// Initializes a new instance of the <see cref="GenerateCompletionRequestBase"/> class with the specified model and prompt.  
    /// </summary>  
    /// <param name="model">The model name.</param>  
    /// <param name="prompt"> The prompt to generate a response for.</param>  
    [SetsRequiredMembers]
    public GenerateCompletionRequestBase(string model, string prompt)
    {
        this.Model = model;
        this.Prompt = prompt;
    }

    /// <summary>  
    /// Initializes a new instance of the <see cref="GenerateCompletionRequestBase"/> class with the specified options.  
    /// </summary>
    /// <param name="options">The options to use for generating completions, including model, prompt, and additional settings.</param>
    [SetsRequiredMembers]
    public GenerateCompletionRequestBase(GenerateCompletionOptions options)
    {
        this.Model = options.Model;
        this.Prompt = options.Prompt;
        this.Images = options.Images;
        this.Format = options.Format;
        this.System = options.System;
        this.Template = options.Template;
        this.Context = options.Context;
        this.Raw = options.Raw;
        this.KeepAlive = options.KeepAlive;
        this.Options = options.Options;
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
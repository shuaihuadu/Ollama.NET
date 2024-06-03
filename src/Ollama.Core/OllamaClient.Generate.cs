﻿namespace Ollama.Core;

public sealed partial class OllamaClient
{
    /// <summary>
    /// Get streaming results for the given prompt.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="prompt">The prompt.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Streaming list of completion result generated by the model</returns>
    public async Task<StreamingResponse<GenerateCompletionResponse>> GenerateCompletionStreamingAsync(string model, string prompt, CancellationToken cancellationToken = default)
    {
        return await this.GenerateCompletionStreamingAsync(new GenerateCompletionStreamingRequest(model, prompt), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get completions use specified model for the given prompt.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="prompt">The prompt.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Completion result generated by the model</returns>
    public async Task<GenerateCompletionResponse> GenerateCompletionAsync(string model, string prompt, CancellationToken cancellationToken = default)
    {
        return await this.GenerateCompletionAsync(new GenerateCompletionRequest(model, prompt), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get completions use specified model for the given prompt.
    /// </summary>
    /// <param name="options">The options to use for generating completions, including model, prompt, and additional settings.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Completion result generated by the model</returns>
    public async Task<GenerateCompletionResponse> GenerateCompletionAsync(GenerateCompletionOptions options, CancellationToken cancellationToken = default)
    {
        return await this.GenerateCompletionAsync(new GenerateCompletionRequest(options), cancellationToken);
    }

    #region Privates

    /// <summary>
    /// Get streaming results for the generate completion using the specified request.
    /// </summary>
    /// <param name="request">The data for this completions request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Streaming list of completion result generated by the model</returns>
    private async Task<StreamingResponse<GenerateCompletionResponse>> GenerateCompletionStreamingAsync(GenerateCompletionStreamingRequest request, CancellationToken cancellationToken = default)
    {
        this._logger.LogDebug("Generate completion streaming: {Model}", request.Model);

        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrWhiteSpace(request.Prompt, nameof(request.Prompt));

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string ResponseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Generate completion streaming response content: {ResponseContent}", response.ResponseContent);

            return StreamingResponse<GenerateCompletionResponse>.CreateFromResponse(response.HttpResponseMessage, (responseMessage) => ServerSendEventAsyncEnumerator<GenerateCompletionResponse>.EnumerateFromSseStream(responseMessage, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for generate completion streaming faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Get completions as configured for the given request.
    /// </summary>
    /// <param name="request">The data for this completions request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Completion result generated by the model</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    private async Task<GenerateCompletionResponse> GenerateCompletionAsync(GenerateCompletionRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrWhiteSpace(request.Prompt, nameof(request.Prompt));

        this._logger.LogDebug("Generate completion: {Model}", request.Model);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Generate completion response content: {ResponseContent}", responseContent);

            GenerateCompletionResponse? generateCompletion = responseContent.FromJson<GenerateCompletionResponse>();

            return generateCompletion is null || string.IsNullOrWhiteSpace(generateCompletion.Model)
                ? throw new DeserializationException(responseContent, message: $"The generate completion response content: '{responseContent}' cannot be deserialize to an instance of {nameof(GenerateCompletionResponse)}.", innerException: null)
                : generateCompletion;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for generate completion faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    #endregion
}
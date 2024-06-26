﻿namespace Ollama.Core;

public sealed partial class OllamaClient
{
    /// <summary>
    /// Get chat completion streaming results for the given chat messages history.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="messages">The chat messages history.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Streaming <see cref="ChatMessage"/> list of completion result replied by the model</returns>
    public async Task<StreamingResponse<ChatCompletionResponse>> ChatCompletionStreamingAsync(string model, ChatMessageHistory messages, CancellationToken cancellationToken = default)
    {
        return await this.ChatCompletionStreamingAsync(new ChatCompletionRequest(model, messages, true), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get chat completions use specified model for the given prompt.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="messages">The chat message history.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Chat Completions replied by the model</returns>
    public async Task<ChatCompletionResponse> ChatCompletionAsync(string model, ChatMessageHistory messages, CancellationToken cancellationToken = default)
    {
        return await this.ChatCompletionAsync(new ChatCompletionRequest(model, messages, false), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get chat completions streaming use the given <see cref="ChatCompletionOptions"/>.
    /// </summary>
    /// <param name="options">The options to use for chat completions.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Chat Completions replied by the model</returns>
    public async Task<StreamingResponse<ChatCompletionResponse>> ChatCompletionStreamingAsync(ChatCompletionOptions options, CancellationToken cancellationToken = default)
    {
        return await this.ChatCompletionStreamingAsync(new ChatCompletionRequest(options), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get chat completions use the given <see cref="ChatCompletionOptions"/>.
    /// </summary>
    /// <param name="options">The options to use for chat completions.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Chat Completions replied by the model</returns>
    public async Task<ChatCompletionResponse> ChatCompletionAsync(ChatCompletionOptions options, CancellationToken cancellationToken = default)
    {
        return await this.ChatCompletionAsync(new ChatCompletionRequest(options), cancellationToken).ConfigureAwait(false);
    }

    #region Privates

    /// <summary>
    /// Get streaming results for the chat request using the specified request.
    /// </summary>
    /// <param name="request">The data for this completions request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Streaming <see cref="ChatMessage"/> list of completion result replied by the model</returns>
    private async Task<StreamingResponse<ChatCompletionResponse>> ChatCompletionStreamingAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        this._logger.LogDebug("Chat completion streaming: {Model}", request.Model);

        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrEmpty(request.Messages, nameof(request.Messages));

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string ResponseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Chat completion streaming response content: {ResponseContent}", response.ResponseContent);

            return StreamingResponse<ChatCompletionResponse>.CreateFromResponse(response.HttpResponseMessage, (responseMessage) => ServerSendEventAsyncEnumerator<ChatCompletionResponse>.EnumerateFromSseStreamAsync(responseMessage, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for chat completion streaming faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Get chat completions as configured for the given request.
    /// </summary>
    /// <param name="request">The data for this chat completion request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Chat message replied by the model</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    private async Task<ChatCompletionResponse> ChatCompletionAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrEmpty(request.Messages, nameof(request.Messages));

        this._logger.LogDebug("Chat completion: {Model}", request.Model);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Chat completion response content: {ResponseContent}", responseContent);

            ChatCompletionResponse? chatCompletion = responseContent.FromJson<ChatCompletionResponse>();

            return chatCompletion is null || string.IsNullOrWhiteSpace(chatCompletion.Model)
                ? throw new DeserializationException(responseContent, message: $"The chat completion response content: '{responseContent}' cannot be deserialize to an instance of {nameof(ChatCompletionResponse)}.", innerException: null)
                : chatCompletion;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for chat completion faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    #endregion
}
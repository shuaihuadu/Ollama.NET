﻿namespace Ollama.Core;

public sealed partial class OllamaClient
{
    /// <summary>
    /// Get streaming results for the given chat messages history.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="chatMessageHistory">The chat messages history.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Streaming <see cref="ChatMessage"/> list of completion result replied by the model</returns>
    public async Task<StreamingResponse<ChatCompletion>> ChatCompletionStreamingAsync(string model, ChatMessageHistory chatMessageHistory, CancellationToken cancellationToken = default)
    {
        return await this.ChatCompletionStreamingAsync(new ChatStreamingCompletionRequest
        {
            Model = model,
            Messages = chatMessageHistory
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get streaming results for the chat request using the specified request.
    /// </summary>
    /// <param name="request">The data for this completions request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Streaming <see cref="ChatMessage"/> list of completion result replied by the model</returns>
    public async Task<StreamingResponse<ChatCompletion>> ChatCompletionStreamingAsync(ChatStreamingCompletionRequest request, CancellationToken cancellationToken = default)
    {
        this._logger.LogDebug("Chat streaming completion");

        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrEmpty(request.Messages, nameof(request.Messages));

        try
        {
            HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string ResponseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Chat streaming completion response content: {responseContent}", response.ResponseContent);

            return StreamingResponse<ChatCompletion>.CreateFromResponse(response.HttpResponseMessage, (responseMessage) => ServerSendEventAsyncEnumerator<ChatCompletion>.EnumerateFromSseStream(responseMessage, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for chat completion streaming faild. {Message}", ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Get completions use specified model for the given prompt.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="prompt">The prompt.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Completion result generated by the model</returns>
    public async Task<ChatCompletion> ChatCompletionAsync(string model, ChatMessageHistory messages, CancellationToken cancellationToken = default)
    {
        return await this.ChatCompletionAsync(new ChatCompletionRequest
        {
            Model = model,
            Messages = messages
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get chat completions as configured for the given request.
    /// </summary>
    /// <param name="request">The data for this chat completion request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Chat message replied by the model</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    public async Task<ChatCompletion> ChatCompletionAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrEmpty(request.Messages, nameof(request.Messages));

        this._logger.LogDebug("Chat completion");

        try
        {
            HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage httpResponseMessage, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Chat completion response content: {responseContent}", responseContent);

            ChatCompletion? chatCompletion = responseContent.FromJson<ChatCompletion>();

            return chatCompletion is null || string.IsNullOrWhiteSpace(chatCompletion.Model)
                ? throw new DeserializationException(responseContent, message: $"The chat completion response content: '{responseContent}' cannot be deserialize to an instance of {nameof(ChatCompletion)}.", innerException: null)
                : chatCompletion;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for chat completion faild. {Message}", ex.Message);

            throw;
        }
    }
}
namespace Ollama.Core;

public sealed partial class OllamaClient
{
    /// <summary>
    /// Generate embeddings from a model.
    /// </summary>
    /// <param name="model">Name of model to generate embeddings from.</param>
    /// <param name="prompt">Text to generate embeddings for.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The embedding.</returns>
    public async Task<EmbeddingResponse> GenerateEmbeddingAsync(string model, string prompt, CancellationToken cancellationToken = default)
    {
        return await GenerateEmbeddingAsync(new GenerateEmbeddingRequest
        {
            Model = model,
            Prompt = prompt
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Generate embeddings from a model.
    /// </summary>
    /// <param name="request">The generate embedding request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The embedding.</returns>
    private async Task<EmbeddingResponse> GenerateEmbeddingAsync(GenerateEmbeddingRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrWhiteSpace(request.Prompt, nameof(request.Prompt));

        this._logger.LogDebug("Generate embedding: {Model}", request.Model);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Generate embedding response content: {ResponseContent}", responseContent);

            EmbeddingResponse? response = responseContent.FromJson<EmbeddingResponse>();

            return response is null || response.Embedding.IsEmpty
                ? throw new DeserializationException(responseContent, message: $"The generate embedding response content: '{responseContent}' cannot be deserialize to an instance of {nameof(EmbeddingResponse)}.", innerException: null)
                : response;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for generate embedding faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }
}
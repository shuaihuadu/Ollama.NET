using Ollama.Core.ServerSendEvent;

namespace Ollama.Core;

public sealed partial class OllamaClient
{
    public async Task GenerateCompletionAsync(string model, string prompt, CancellationToken cancellationToken = default)
    {
        await this.GenerateCompletionAsync(new GenerateCompletionRequest
        {
            Model = model,
            Prompt = prompt,
            Stream = false
        }, cancellationToken).ConfigureAwait(false);
    }
    public async Task<StreamingResponse<StreamingCompletionUpdate>> GenerateStreamingCompletionAsync(string model, string prompt, CancellationToken cancellationToken = default)
    {
        return await this.GenerateStreamingCompletionAsync(new GenerateCompletionRequest
        {
            Model = model,
            Prompt = prompt,
            Stream = true
        }, cancellationToken).ConfigureAwait(false);
    }

    public async Task<StreamingResponse<StreamingCompletionUpdate>> GenerateStreamingCompletionAsync(GenerateCompletionRequest request, CancellationToken cancellationToken = default)
    {
        this._logger.LogDebug("Generate streaming completion");

        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrWhiteSpace(request.Prompt, nameof(request.Prompt));

        try
        {
            HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string ResponseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            return StreamingResponse<StreamingCompletionUpdate>.CreateFromResponse(response.HttpResponseMessage, (responseMessageForEnumeration) => ServerSendEventAsyncEnumerator<StreamingCompletionUpdate>.EnumerateFromSseStream(responseMessageForEnumeration, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for generate completion faild {Message}", ex.Message);

            throw;
        }
    }
    public async Task GenerateCompletionAsync(GenerateCompletionRequest request, CancellationToken cancellationToken = default)
    {
        this._logger.LogDebug("Generate completion");

        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));
        Argument.AssertNotNullOrWhiteSpace(request.Prompt, nameof(request.Prompt));

        string? responseContent = null;

        try
        {
            HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace(responseContent);
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for generate completion faild {Message}", ex.Message);

            throw;
        }
    }

}
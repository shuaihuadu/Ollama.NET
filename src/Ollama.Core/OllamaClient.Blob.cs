namespace Ollama.Core;

public sealed partial class OllamaClient
{
    /// <summary>
    /// Ensures that the file blob used for a FROM or ADAPTER field exists on the server. <br />
    /// This is checking your Ollama server and not Ollama.ai.
    /// </summary>
    /// <param name="digest">The SHA256 digest of the blob.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>Return true if the blob exists, otherwise if it does not.</returns>
    public async Task<bool> CheckBlobExistsAsync(string digest, CancellationToken cancellationToken = default)
    {
        return await CheckBlobExistsAsync(new CheckBlobExistsRequest
        {
            Digest = digest
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Create a blob from a file on the server. Returns the server file path.
    /// </summary>
    /// <param name="digest">The SHA256 digest of the blob.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    public async Task CreateBlobAsync(string digest, byte[] content, CancellationToken cancellationToken = default)
    {
        await CreateBlobAsync(new CreateBlobRequest
        {
            Digest = digest,
            Content = content
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Ensures that the file blob used for a FROM or ADAPTER field exists on the server. <br />
    /// This is checking your Ollama server and not Ollama.ai.
    /// </summary>
    /// <param name="request">The blob exists check request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <remarks>Return 200 OK if the blob exists, 404 Not Found if it does not.</remarks>
    /// <returns>Return true if the blob exists, otherwise if it does not.</returns>
    private async Task<bool> CheckBlobExistsAsync(CheckBlobExistsRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrEmpty(request.Digest, nameof(request.Digest));

        this._logger.LogDebug("Check blob exists: {Digest}", request.Digest);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Check blob exists response content: {responseContent}", responseContent);

            return HttpResponseMessage.StatusCode == HttpStatusCode.OK;
        }
        catch (HttpOperationException ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            this._logger.LogError(ex, "Check blob exists faild. {Message}", ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Create a blob from a file on the server. Returns the server file path.
    /// </summary>
    /// <param name="request">The blob create request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <remarks>Return 201 Created if the blob was successfully created, 400 Bad Request if the digest used is not expected.</remarks>
    private async Task CreateBlobAsync(CreateBlobRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Digest, nameof(request.Digest));
        Argument.AssertNotNull(request.Content, nameof(request.Content));

        this._logger.LogDebug("Create blob Digest: {Digest}", request.Digest);

        try
        {
            using ByteArrayContent content = new(request.Content);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            requestMessage.Content = content;

            (HttpResponseMessage HttpResponseMessage, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Create blob response content: {responseContent}", responseContent);
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Create blob faild. {Message}", ex.Message);

            throw;
        }
    }
}
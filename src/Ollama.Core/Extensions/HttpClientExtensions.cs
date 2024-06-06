namespace Ollama.Core.Extensions;

internal static class HttpClientExtensions
{
    /// <summary>
    /// Sends an HTTP request using the provided <see cref="HttpClient"/> instance and checks for a successful response.
    /// If the response is not successful, it logs an error and throws an <see cref="HttpOperationException"/>.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> instance to use for sending the request.</param>
    /// <param name="request">The <see cref="HttpRequestMessage"/> to send.</param>
    /// <param name="completionOption">Indicates if HttpClient operations should be considered completed either as soon as a response is available,
    /// or after reading the entire response message including the content.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for canceling the request.</param>
    /// <returns>The <see cref="HttpResponseMessage"/> representing the response.</returns>
    internal static async Task<HttpResponseMessage> SendWithSuccessCheckAsync(this HttpClient client, HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        HttpResponseMessage? response;
        try
        {
            response = await client.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException e)
        {
            throw new HttpOperationException(HttpStatusCode.BadRequest, null, e.Message, e);
        }

        if (!response.IsSuccessStatusCode)
        {
            string? responseContent = null;
            try
            {
                responseContent = await response!.Content.ReadAsStringAsync().ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new HttpOperationException(response.StatusCode, responseContent, ex.Message, ex);
            }
        }

        return response;
    }

    /// <summary>
    /// Sends an HTTP request using the provided <see cref="HttpClient"/> instance and checks for a successful response.
    /// If the response is not successful, it logs an error and throws an <see cref="HttpOperationException"/>.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> instance to use for sending the request.</param>
    /// <param name="request">The <see cref="HttpRequestMessage"/> to send.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for canceling the request.</param>
    /// <returns>The <see cref="HttpResponseMessage"/> representing the response.</returns>
    internal static async Task<HttpResponseMessage> SendWithSuccessCheckAsync(this HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await client.SendWithSuccessCheckAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
    }
}

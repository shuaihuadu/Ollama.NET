namespace Ollama.Core.ServerSendEvent;

/// <summary>
/// The server send event async enumerator.
/// </summary>
/// <typeparam name="T">The type of the objects being enumerated.</typeparam>  
internal static class ServerSendEventAsyncEnumerator<T> where T : class
{
    /// <summary>  
    /// Asynchronously enumerates through a Server-Sent Events (SSE) stream from an HTTP response message.  
    /// </summary>  
    /// <param name="httpResponseMessage">The HTTP response message containing the SSE stream.</param>  
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>An asynchronous enumerable of type <typeparamref name="T"/> representing the updates from the SSE stream.</returns>  
    /// <remarks>  
    /// This method reads from the SSE stream until the end of the stream is reached or the operation is cancelled.  
    /// It uses <see cref="HttpResponseMessage.Content"/> to obtain the stream and <see cref="ServerSendEventReader{T}"/> to parse SSE events.  
    /// </remarks>
    internal static async IAsyncEnumerable<T> EnumerateFromSseStream(HttpResponseMessage httpResponseMessage, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

        using StreamReader streamReader = new(stream);

        using ServerSendEventReader<T> serverSendEventReader = new(streamReader);

        while (!streamReader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            T? update = await serverSendEventReader.TryReadLineAsync().ConfigureAwait(false);

            if (update is not null)
            {
                yield return update;
            }
        }

    }
}

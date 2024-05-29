namespace Ollama.Core.ServerSendEvent;

internal static class ServerSendEventAsyncEnumerator<T> where T : class
{
    internal static async IAsyncEnumerable<T> EnumerateFromSseStream(HttpResponseMessage httpResponseMessage, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {

        using Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

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

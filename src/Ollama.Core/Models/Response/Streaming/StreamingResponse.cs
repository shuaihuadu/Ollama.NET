namespace Ollama.Core.Models;

/// <summary>
/// Represents an operation response with streaming content that can be deserialized and enumerated while the response
/// is still being received.
/// </summary>
/// <typeparam name="T"> The data type representative of distinct, streamable items. </typeparam>
public class StreamingResponse<T> : IDisposable, IAsyncEnumerable<T>
{
    private HttpResponseMessage HttpResponseMessage { get; } = null!;
    private IAsyncEnumerable<T> AsyncEnumerableSource { get; } = null!;
    private bool DisposedValue { get; set; }

    private StreamingResponse() { }

    private StreamingResponse(
        HttpResponseMessage httpResponseMessage,
        Func<HttpResponseMessage, IAsyncEnumerable<T>> asyncEnumerableProcessor)
    {
        HttpResponseMessage = httpResponseMessage;
        AsyncEnumerableSource = asyncEnumerableProcessor.Invoke(httpResponseMessage);
    }

    /// <summary>
    /// Creates a new instance of <see cref="StreamingResponse{T}"/> using the provided underlying HTTP response. The
    /// provided function will be used to resolve the response into an asynchronous enumeration of streamed response
    /// items.
    /// </summary>
    /// <param name="response">The HTTP response.</param>
    /// <param name="asyncEnumerableProcessor">
    /// The function that will resolve the provided response into an IAsyncEnumerable.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="StreamingResponse{T}"/> that will be capable of asynchronous enumeration of
    /// <typeparamref name="T"/> items from the HTTP response.
    /// </returns>
    public static StreamingResponse<T> CreateFromResponse(
        HttpResponseMessage response,
        Func<HttpResponseMessage, IAsyncEnumerable<T>> asyncEnumerableProcessor)
    {
        return new(response, asyncEnumerableProcessor);
    }

    /// <summary>
    /// Gets the asynchronously enumerable collection of distinct, streamable items in the response.
    /// </summary>
    /// <remarks>
    /// <para> The return value of this method may be used with the "await foreach" statement. </para>
    /// <para>
    /// As <see cref="StreamingResponse{T}"/> explicitly implements <see cref="IAsyncEnumerable{T}"/>, callers may
    /// enumerate a <see cref="StreamingResponse{T}"/> instance directly instead of calling this method.
    /// </para>
    /// </remarks>
    /// <returns></returns>
    public IAsyncEnumerable<T> EnumerateValues() => this;

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
        if (!DisposedValue)
        {
            if (disposing)
            {
                this.HttpResponseMessage?.Dispose();
            }

            DisposedValue = true;
        }
    }

    IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken) => AsyncEnumerableSource?.GetAsyncEnumerator(cancellationToken)!;
}

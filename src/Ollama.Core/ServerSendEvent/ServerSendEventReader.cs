// Copyright (c) IdeaTech. All rights reserved.

namespace Ollama.Core.ServerSendEvent;

/// <summary>
/// Reads events from a stream and deserializes them into objects of <typeparamref name="T"/>.
/// Initializes a new instance of the <see cref="ServerSendEventReader{T}"/> class with the specified <paramref name="reader"/>.
/// </summary>
/// <typeparam name="T">The type of object to deserialize the events into. Must be a class.</typeparam>
/// <param name="reader">The StreamReader to read events from.</param>
internal sealed class ServerSendEventReader<T>(StreamReader reader) : IDisposable where T : class
{
    private readonly StreamReader _reader = reader;

    private bool _disposedValue;

    /// <summary>
    /// Tries to read a line asynchronously from the StreamReader and deserialize it into an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with a result of the deserialized object of type <typeparamref name="T"/>, or null if the line is empty or cannot be parsed.</returns>
    public async Task<T?> TryReadLineAsync()
    {
        string? lineText = await this._reader.ReadLineAsync().ConfigureAwait(false);

        if (lineText is null)
        {
            return null;
        }

        if (lineText.Length == 0)
        {
            return default;
        }

        if (TryParseLine(lineText, out T? line))
        {
            return line;
        }

        return null;
    }

    /// <summary>
    /// Tries to parse a line of text into an object of type T.
    /// </summary>
    /// <param name="lineText">The line of text to parse.</param>
    /// <param name="line">When this method returns, contains the parsed object of type <typeparamref name="T"/>, if the parsing succeeded, or the default value of <typeparamref name="T"/> if the parsing failed.</param>
    /// <returns>True if the line was successfully parsed; otherwise, false.</returns>
    private static bool TryParseLine(string lineText, out T? line)
    {
        if (lineText.Length == 0)
        {
            line = default;

            return false;
        }

        line = JsonSerializer.Deserialize<T>(lineText);

        return true;
    }

    /// <summary>
    /// Releases the unmanaged resources used by the ServerSendEventReader and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    private void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                this._reader.Dispose();
            }

            this._disposedValue = true;
        }
    }

    /// <summary>
    /// Releases all resources used by the ServerSendEventReader.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);

        GC.SuppressFinalize(this);
    }
}

namespace Ollama.Core.ServerSendEvent;

internal sealed class ServerSendEventReader<T>(StreamReader reader) : IDisposable where T : class
{
    private readonly StreamReader _reader = reader;
    private bool _disposedValue;

    public async Task<T?> TryReadLineAsync()
    {
        string? lineText = await _reader.ReadLineAsync().ConfigureAwait(false);

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

    private void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                _reader.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

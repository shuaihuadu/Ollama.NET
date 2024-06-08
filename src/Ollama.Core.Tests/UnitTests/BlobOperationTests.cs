namespace Ollama.Core.Tests.UnitTests;

public sealed class BlobOperationTests : IDisposable
{
    private readonly HttpMessageHandlerStub _messageHandlerStub;
    private readonly HttpClient _httpClient;

    public BlobOperationTests()
    {
        this._messageHandlerStub = new HttpMessageHandlerStub();
        this._httpClient = new HttpClient(this._messageHandlerStub, false);
    }

    public void Dispose()
    {
        this._messageHandlerStub.Dispose();
        this._httpClient.Dispose();
    }
}

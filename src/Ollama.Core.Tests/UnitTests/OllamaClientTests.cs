namespace Ollama.Core.Tests.UnitTests;

#pragma warning disable CS8604 // Possible null reference argument.
public sealed partial class OllamaClientTests
{
    private readonly Mock<ILoggerFactory> _mockLoggerFactory;
    private readonly HttpClient _httpClient;

    private const string MockUriString = "http://mock.url";
    private static readonly Uri MockUri = new(MockUriString);


    private static readonly HttpClient? NullHttpClient = null;
    private static readonly Uri? NullUri = null;

    public OllamaClientTests()
    {
        this._mockLoggerFactory = new Mock<ILoggerFactory>();
        this._httpClient = new HttpClient();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithUri(bool includeLoggerFactory)
    {
        using OllamaClient ollamaClient = includeLoggerFactory
            ? new OllamaClient(MockUri, this._mockLoggerFactory.Object)
            : new OllamaClient(MockUri);

        Assert.NotNull(ollamaClient);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithUriString(bool includeLoggerFactory)
    {
        using OllamaClient ollamaClient = includeLoggerFactory
            ? new OllamaClient(MockUriString, this._mockLoggerFactory.Object)
            : new OllamaClient(MockUriString);

        Assert.NotNull(ollamaClient);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithHttpClient(bool includeLoggerFactory)
    {
        this._httpClient.BaseAddress = MockUri;

        using OllamaClient ollamaClient = includeLoggerFactory
            ? new OllamaClient(this._httpClient, this._mockLoggerFactory.Object)
            : new OllamaClient(this._httpClient);

        Assert.NotNull(ollamaClient);
    }
}
#pragma warning restore CS8604 // Possible null reference argument.
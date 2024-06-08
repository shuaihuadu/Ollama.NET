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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithHttpClientAndUri(bool includeLoggerFactory)
    {
        using OllamaClient ollamaClient1 = includeLoggerFactory
            ? new OllamaClient(this._httpClient, MockUri, this._mockLoggerFactory.Object)
            : new OllamaClient(this._httpClient, MockUri);

        Assert.NotNull(ollamaClient1);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithHttpClientAndNullUri(bool includeLoggerFactory)
    {
        this._httpClient.BaseAddress = MockUri;

        //Uri is null
        using OllamaClient ollamaClient2 = includeLoggerFactory
            ? new OllamaClient(this._httpClient, NullUri, this._mockLoggerFactory.Object)
            : new OllamaClient(this._httpClient, NullUri);

        Assert.NotNull(ollamaClient2);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithNullHttpClientAndUri(bool includeLoggerFactory)
    {
        this._httpClient.BaseAddress = MockUri;

        //HttpClient is null
        using OllamaClient ollamaClient2 = includeLoggerFactory
            ? new OllamaClient(NullHttpClient, MockUri, this._mockLoggerFactory.Object)
            : new OllamaClient(NullHttpClient, MockUri);

        Assert.NotNull(ollamaClient2);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithHttpClientAndUriString(bool includeLoggerFactory)
    {
        using OllamaClient ollamaClient = includeLoggerFactory
            ? new OllamaClient(this._httpClient, MockUriString, this._mockLoggerFactory.Object)
            : new OllamaClient(this._httpClient, MockUriString);

        Assert.NotNull(ollamaClient);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithHttpClientAndInvalidUriString(bool includeLoggerFactory)
    {
        this._httpClient.BaseAddress = MockUri;

        //Uri string is null / empty
        using OllamaClient ollamaClient = includeLoggerFactory
            ? new OllamaClient(this._httpClient, string.Empty, this._mockLoggerFactory.Object)
            : new OllamaClient(this._httpClient, string.Empty);

        Assert.NotNull(ollamaClient);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsWorkCorrectlyWithNullHttpClientAndUriString(bool includeLoggerFactory)
    {
        //HttpClient is null
        using OllamaClient ollamaClient = includeLoggerFactory
            ? new OllamaClient(NullHttpClient, MockUriString, this._mockLoggerFactory.Object)
            : new OllamaClient(NullHttpClient, MockUriString);

        Assert.NotNull(ollamaClient);
    }
}
#pragma warning restore CS8604 // Possible null reference argument.
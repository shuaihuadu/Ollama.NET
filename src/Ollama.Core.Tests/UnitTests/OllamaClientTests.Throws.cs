namespace Ollama.Core.Tests.UnitTests;

#pragma warning disable CS8604 // Possible null reference argument.
public sealed partial class OllamaClientTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsThrowsCorrectlyWithNullUri(bool includeLoggerFactory)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient(NullUri, this._mockLoggerFactory.Object)
                : new OllamaClient(NullUri);
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsThrowsCorrectlyWithInvalidlUriString(bool includeLoggerFactory)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient(string.Empty, this._mockLoggerFactory.Object)
                : new OllamaClient(string.Empty);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient("invalid url", this._mockLoggerFactory.Object)
                : new OllamaClient("invalid url");
        });
    }


    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsThrowsCorrectlyWithInvalidlHttpClient(bool includeLoggerFactory)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient(NullHttpClient, this._mockLoggerFactory.Object)
                : new OllamaClient(NullHttpClient);
        });

        //_httpClint.BaseAddress is null
        Assert.Throws<ArgumentNullException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient(this._httpClient, this._mockLoggerFactory.Object)
                : new OllamaClient(this._httpClient);
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsThrowsCorrectlyWithNullHttpClientAndNullUri(bool includeLoggerFactory)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient(NullHttpClient, NullUri, this._mockLoggerFactory.Object)
                : new OllamaClient(NullHttpClient, NullUri);
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ConstructsThrowsCorrectlyWithNullHttpClientAndInvalidUriString(bool includeLoggerFactory)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient(NullHttpClient, string.Empty, this._mockLoggerFactory.Object)
                : new OllamaClient(NullHttpClient, string.Empty);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            using OllamaClient ollamaClient = includeLoggerFactory
                ? new OllamaClient(NullHttpClient, "invalid url", this._mockLoggerFactory.Object)
                : new OllamaClient(NullHttpClient, "invalid url");
        });
    }

}
#pragma warning restore CS8604 // Possible null reference argument.

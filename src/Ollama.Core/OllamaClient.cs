namespace Ollama.Core;

/// <summary>
/// An implementation of a client for the Ollama serve.
/// </summary>
public sealed partial class OllamaClient
{
    private readonly Uri _endpoint;
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaClient"/> class.
    /// </summary>
    /// <param name="endpoint">The Ollama serve endpoint.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public OllamaClient(Uri endpoint, ILoggerFactory? loggerFactory = null)
    {
        Argument.AssertNotNull(endpoint, nameof(endpoint));

        this._endpoint = endpoint;
        this._httpClient = new HttpClient
        {
            BaseAddress = endpoint
        };
        this._logger = loggerFactory?.CreateLogger(typeof(OllamaClient)) ?? NullLogger.Instance;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaClient"/> class.
    /// </summary>
    /// <param name="endpoint">The Ollama serve endpoint.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public OllamaClient(string endpoint, ILoggerFactory? loggerFactory = null)
        : this(new Uri(endpoint), loggerFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaClient"/> class.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance used for making HTTP requests.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public OllamaClient(HttpClient httpClient, ILoggerFactory? loggerFactory = null)
    {
        Argument.AssertNotNull(httpClient, nameof(httpClient));
        Argument.AssertNotNull(httpClient.BaseAddress, nameof(httpClient.BaseAddress));
        Argument.AssertNotNullOrWhiteSpace(httpClient.BaseAddress!.AbsoluteUri, nameof(httpClient.BaseAddress));

        this._httpClient = httpClient;
        this._endpoint = httpClient.BaseAddress;
        this._logger = loggerFactory?.CreateLogger(typeof(OllamaClient)) ?? NullLogger.Instance;
    }

    private async Task<(HttpResponseMessage, string)> ExecuteHttpRequestAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await this._httpClient.SendWithSuccessCheckAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        string responseContent = await response.Content.ReadAsStringWithExceptionMappingAsync().ConfigureAwait(false);

        return (response, responseContent);
    }
}
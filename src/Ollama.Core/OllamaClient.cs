namespace Ollama.Core;

/// <summary>
/// An implementation of a client for the Ollama serve.
/// </summary>
public sealed partial class OllamaClient : IDisposable
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
        : this(SanitizeEndpoint(endpoint), loggerFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaClient"/> class.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance used for making HTTP requests.</param>
    /// <param name="endpoint">The Ollama serve endpoint.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public OllamaClient(HttpClient? httpClient, Uri? endpoint, ILoggerFactory? loggerFactory = null)
    {
        Verify.ValidateHttpClientAndEndpoint(httpClient, endpoint);

        this._httpClient ??= new HttpClient
        {
            BaseAddress = endpoint
        };

        this._httpClient.BaseAddress ??= endpoint;
        this._endpoint = endpoint ?? this._httpClient.BaseAddress!;
        this._logger = loggerFactory?.CreateLogger(typeof(OllamaClient)) ?? NullLogger.Instance;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaClient"/> class.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance used for making HTTP requests.</param>
    /// <param name="endpoint">The Ollama serve endpoint.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public OllamaClient(HttpClient? httpClient, string? endpoint, ILoggerFactory? loggerFactory = null)
    {
        Verify.ValidateHttpClientAndEndpoint(httpClient, endpoint);

        Uri endpointUri = httpClient?.BaseAddress ?? new Uri(endpoint!);

        this._httpClient ??= new HttpClient
        {
            BaseAddress = endpointUri
        };

        this._httpClient.BaseAddress ??= endpointUri;
        this._endpoint = endpointUri;
        this._logger = loggerFactory?.CreateLogger(typeof(OllamaClient)) ?? NullLogger.Instance;
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

    /// <inheritdoc />
    public void Dispose() => this._httpClient.Dispose();

    private static Uri SanitizeEndpoint(string? endpoint, int? port = null)
    {
        Verify.ValidateUrl(endpoint);

        UriBuilder builder = new(endpoint!);
        if (port.HasValue) { builder.Port = port.Value; }

        return builder.Uri;
    }

    private async Task<(HttpResponseMessage, string)> ExecuteHttpRequestAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await this._httpClient.SendWithSuccessCheckAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        string responseContent = await response.Content.ReadAsStringWithExceptionMappingAsync().ConfigureAwait(false);

        return (response, responseContent);
    }
}
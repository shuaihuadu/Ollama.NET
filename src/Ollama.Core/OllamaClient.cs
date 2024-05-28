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
        this._httpClient = new HttpClient();
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
        Argument.AssertNotNullOrWhiteSpace(endpoint, nameof(endpoint));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaClient"/> class.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance used for making HTTP requests.</param>
    /// <param name="endpoint">The Ollama serve endpoint.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public OllamaClient(HttpClient httpClient, Uri endpoint, ILoggerFactory? loggerFactory = null)
        : this(endpoint, loggerFactory)
    {
        this._httpClient = httpClient;
        this._endpoint = endpoint;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaClient"/> class.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance used for making HTTP requests.</param>
    /// <param name="endpoint">The Ollama serve endpoint.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    public OllamaClient(HttpClient httpClient, string endpoint, ILoggerFactory? loggerFactory = null)
        : this(httpClient, SanitizeEndpoint(endpoint), loggerFactory)
    {

    }

    public void Dispose() => this._httpClient.Dispose();


    private static Uri SanitizeEndpoint(string endpoint, int? port = null)
    {
        Verify.ValidateUrl(endpoint);

        UriBuilder builder = new(endpoint);
        if (port.HasValue) { builder.Port = port.Value; }

        return builder.Uri;
    }
}

﻿namespace Ollama.Core;

/// <summary>
/// Represents an exception specific to HTTP operations.
/// </summary>
public class HttpOperationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpOperationException"/> class.
    /// </summary>
    public HttpOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpOperationException"/> class with its message set to <paramref name="message"/>.
    /// </summary>
    /// <param name="message">A string that describes the error.</param>
    public HttpOperationException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpOperationException"/> class with its message set to <paramref name="message"/>.
    /// </summary>
    /// <param name="message">A string that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public HttpOperationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpOperationException"/> class with its message
    /// and additional properties for the HTTP status code and response content.
    /// </summary>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="responseContent">The content of the HTTP response.</param>
    /// <param name="message">A string that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public HttpOperationException(HttpStatusCode? statusCode, string? responseContent, string? message, Exception? innerException)
        : base(message, innerException)
    {
        this.StatusCode = statusCode;
        this.ResponseContent = responseContent;
    }

    /// <summary>
    /// Gets or sets the HTTP status code. If the property is null, it indicates that no response was received.
    /// </summary>
    public HttpStatusCode? StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the content of the HTTP response.
    /// </summary>
    public string? ResponseContent { get; set; }

    /// <summary>
    /// Gets the method used for the HTTP request.
    /// </summary>
    /// <remarks>
    /// This information is only available in limited circumstances e.g. when using Open API plugins.
    /// </remarks>
    public string? RequestMethod { get; set; }

    /// <summary>
    /// Gets the System.Uri used for the HTTP request.
    /// </summary>
    /// <remarks>
    /// This information is only available in limited circumstances e.g. when using Open API plugins.
    /// </remarks>
    public Uri? RequestUri { get; set; }

    /// <summary>
    /// Gets the payload sent in the request.
    /// </summary>
    /// <remarks>
    /// This information is only available in limited circumstances e.g. when using Open API plugins.
    /// </remarks>
    public object? RequestPayload { get; set; }
}

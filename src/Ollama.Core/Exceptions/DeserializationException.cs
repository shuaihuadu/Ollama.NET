namespace Ollama.Core;

/// <summary>
/// Represents an exception specific to when json deserialization is null.
/// </summary>
public class DeserializationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeserializationException"/> class.
    /// </summary>
    public DeserializationException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeserializationException"/> class with its message set to <paramref name="message"/>.
    /// </summary>
    /// <param name="message">A string that describes the error.</param>
    public DeserializationException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeserializationException"/> class with its message set to <paramref name="message"/>.
    /// </summary>
    /// <param name="message">A string that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public DeserializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeserializationException"/> class with its json content.
    /// </summary>
    /// <param name="json">The json string to deserialize.</param>
    /// <param name="message">A string that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public DeserializationException(string json, string? message, Exception? innerException)
        : base(message, innerException)
    {
        this.Json = json;
    }

    /// <summary>
    /// Gets or sets the content of json.
    /// </summary>
    public string? Json { get; set; }
}

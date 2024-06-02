namespace Ollama.Core.Models;

/// <summary>
/// A model including details, modelfile, template, parameters, license, and system prompt.
/// </summary>
public class ListModelItem
{
    /// <summary>
    /// The model name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The
    /// </summary>
    [JsonPropertyName("modified_at")]
    public DateTimeOffset ModifiedAt { get; set; }

    /// <summary>
    /// The model size in byte.
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// The model SHA256 digest.
    /// </summary>
    [JsonPropertyName("digest")]
    public string? Digest { get; set; }

    /// <summary>
    /// The model details.
    /// </summary>
    [JsonPropertyName("details")]
    public ModelDetail? Details { get; set; }
}
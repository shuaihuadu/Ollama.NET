namespace Ollama.Core.Models;

/// <summary>
/// The pull model response.
/// </summary>
public sealed class PullModelResponse
{
    /// <summary>
    /// The pull status of the model.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// The SHA256 digest of the blob
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("digest")]
    public string? Digest { get; set; }

    /// <summary>
    /// The total size of the model.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("total")]
    public double? Total { get; set; }

    /// <summary>
    /// The size of the completed pull.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("completed")]
    public double? Completed { get; set; }
}
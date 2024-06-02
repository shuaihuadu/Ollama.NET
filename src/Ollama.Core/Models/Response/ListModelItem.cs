namespace Ollama.Core.Models;

/// <summary>
/// A model including details, modelfile, template, parameters, license, and system prompt.
/// </summary>
public class ListModelItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("modified_at")]
    public DateTimeOffset ModifiedAt { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("digest")]
    public string? Digest { get; set; }

    [JsonPropertyName("details")]
    public ModelDetail? Details { get; set; }
}
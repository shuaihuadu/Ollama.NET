namespace Ollama.Core.Models;

/// <summary>
/// The model details.
/// </summary>
public class ModelDetail
{
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// The model family.
    /// </summary>
    [JsonPropertyName("family")]
    public string? Family { get; set; }

    /// <summary>
    /// The model families.
    /// </summary>
    [JsonPropertyName("families")]
    public List<string>? Families { get; set; }

    /// <summary>
    /// The model parameter size.
    /// </summary>
    [JsonPropertyName("parameter_size")]
    public string? ParameterSize { get; set; }

    /// <summary>
    /// The model quantization level.
    /// </summary>
    [JsonPropertyName("quantization_level")]
    public string? QuantizationLevel { get; set; }
}
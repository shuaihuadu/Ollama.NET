namespace Ollama.Core.Models;

/// <summary>
/// A model including details, modelfile, template, parameters, license, and system prompt.
/// </summary>
public class ShowModelResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("license")]
    public string? License { get; set; }

    /// <summary>
    /// A model file is the blueprint to create and share models with Ollama.
    /// </summary>
    [JsonPropertyName("modelfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ModelFile { get; set; }

    [JsonPropertyName("parameters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Parameters { get; set; }

    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    [JsonPropertyName("details")]
    public ModelDetail? Details { get; set; }
}
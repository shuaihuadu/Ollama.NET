namespace Ollama.Core.Models;

/// <summary>
/// A model including details, modelfile, template, parameters, license, and system prompt.
/// </summary>
public class ShowModelResponse
{
    /// <summary>
    /// The model license content.
    /// </summary>
    [JsonPropertyName("license")]
    public string? License { get; set; }

    /// <summary>
    /// A model file is the blueprint to create and share models with Ollama.
    /// </summary>
    [JsonPropertyName("modelfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ModelFile { get; set; }

    /// <summary>
    /// The parameters is for how Ollama will run the model.
    /// </summary>
    [JsonPropertyName("parameters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Parameters { get; set; }

    /// <summary>
    /// The full prompt template in the model.
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// The model details.
    /// </summary>
    [JsonPropertyName("details")]
    public ModelDetail? Details { get; set; }
}
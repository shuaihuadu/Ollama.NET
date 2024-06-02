namespace Ollama.Core.Models;

/// <summary>
/// The <seealso cref="ParameterOptions"/> defines parameters that can be set when the model is run. <br />
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/modelfile.md#valid-parameters-and-values"/>
/// </summary>
public class ParameterOptions
{
    /// <summary>
    /// Enable Mirostat sampling for controlling perplexity. (default: 0, 0 = disabled, 1 = Mirostat, 2 = Mirostat 2.0)
    /// </summary>
    [JsonPropertyName("mirostat")]
    public int Mirostat { get; set; }

    /// <summary>
    /// Influences how quickly the algorithm responds to feedback from the generated text.
    /// A lower learning rate will result in slower adjustments, while a higher learning rate will make the algorithm more responsive. (Default: 0.1)
    /// </summary>
    [JsonPropertyName("mirostat_eta")]
    public float MirostatEta { get; set; } = 0.1f;

    /// <summary>
    /// Controls the balance between coherence and diversity of the output.
    /// A lower value will result in more focused and coherent text. (Default: 5.0)
    /// </summary>
    [JsonPropertyName("mirostat_tau")]
    public float MirostatTau { get; set; } = 0.5f;

    /// <summary>
    /// Sets the size of the context window used to generate the next token. (Default: 2048)
    /// </summary>
    [JsonPropertyName("num_ctx")]
    public int NumCtx { get; set; } = 2048;

    /// <summary>
    /// Sets how far back for the model to look back to prevent repetition. (Default: 64, 0 = disabled, -1 = num_ctx)
    /// </summary>
    [JsonPropertyName("repeat_last_n")]
    public int RepeatLastN { get; set; } = 64;

    /// <summary>
    /// Sets how strongly to penalize repetitions.
    /// A higher value(e.g., 1.5) will penalize repetitions more strongly, while a lower value(e.g., 0.9) will be more lenient. (Default: 1.1)
    /// </summary>
    [JsonPropertyName("repeat_penalty")]
    public float RepeatPenalty { get; set; } = 1.1f;

    /// <summary>
    /// The temperature of the model.Increasing the temperature will make the model answer more creatively. (Default: 0.8)
    /// </summary>
    [JsonPropertyName("temperature")]
    public float Temperature { get; set; } = 0.8f;

    /// <summary>
    /// Sets the random number seed to use for generation.
    /// Setting this to a specific number will make the model generate the same text for the same prompt. (Default: 0)
    /// </summary>
    [JsonPropertyName("seed")]
    public int Seed { get; set; }

    /// <summary>
    /// Sets the stop sequences to use.When this pattern is encountered the LLM will stop generating text and return.
    /// Multiple stop patterns may be set by specifying multiple separate stop parameters in a modelfile.
    /// </summary>
    [JsonPropertyName("stop")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Stop { get; set; }

    /// <summary>
    /// Tail free sampling is used to reduce the impact of less probable tokens from the output.
    /// A higher value (e.g., 2.0) will reduce the impact more, while a value of 1.0 disables this setting. (default: 1)
    /// </summary>
    [JsonPropertyName("tfs_z")]
    public float TfsZ { get; set; } = 1f;

    /// <summary>
    /// Maximum number of tokens to predict when generating text. (Default: 128, -1 = infinite generation, -2 = fill context)
    /// </summary>
    [JsonPropertyName("num_predict")]
    public int NumPredict { get; set; } = 128;

    /// <summary>
    /// Reduces the probability of generating nonsense.
    /// A higher value(e.g. 100) will give more diverse answers, while a lower value(e.g. 10) will be more conservative. (Default: 40)
    /// </summary>
    [JsonPropertyName("top_k")]
    public int TopK { get; set; } = 40;

    /// <summary>
    /// Works together with top-k.
    /// A higher value(e.g., 0.95) will lead to more diverse text, while a lower value(e.g., 0.5) will generate more focused and conservative text. (Default: 0.9)
    /// </summary>
    [JsonPropertyName("top_p")]
    public float TopP { get; set; } = 0.9f;
}

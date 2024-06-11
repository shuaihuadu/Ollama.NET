namespace Ollama.Core.Models;

/// <summary>
/// The <seealso cref="ParameterOptions"/> defines parameters that can be set when the model is run. <br />
/// <a href="https://github.com/ollama/ollama/blob/main/docs/modelfile.md#valid-parameters-and-values"/>
/// </summary>
public class ParameterOptions
{
    #region Load time options

    /// <summary>
    /// NUMA (Non-Uniform Memory Access) node configuration. This parameter is used to specify the NUMA nodes utilized during model execution to optimize memory access performance. <br />
    /// Invalid parameter for argument chat completion
    /// </summary>
    [JsonPropertyName("numa")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Numa { get; set; }

    /// <summary>
    /// Batch size. This parameter determines the amount of data processed by the model in one go.
    /// </summary>
    [JsonPropertyName("num_batch")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int NumBatch { get; set; }

    /// <summary>
    /// Sets the size of the context window used to generate the next token. (Default: 2048).
    /// </summary>
    /// <remarks>
    /// It doesn't seem to be working right now, reference:<br />
    /// <a href="https://github.com/ollama/ollama-python/issues/76" /><br />
    /// <a href="https://github.com/ollama/ollama-python/issues/84" /><br />
    /// <a href="https://github.com/ollama/ollama-python/issues/86" /><br />
    /// <a href="https://github.com/ollama/ollama/issues/4447" />
    /// </remarks>
    [JsonPropertyName("num_ctx")]
    public int NumCtx { get; set; } = 2048;

    /// <summary>
    /// Number of GPUs used. This parameter specifies the number of GPUs utilized for model training or inference.
    /// </summary>
    [JsonPropertyName("num_gpu")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int NumGpu { get; set; }

    /// <summary>
    /// Main GPU index. This parameter specifies the GPU index used for primary computation tasks.
    /// </summary>
    [JsonPropertyName("main_gpu")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int MainGpu { get; set; }

    /// <summary>
    /// Low VRAM mode. When enabled, this parameter optimizes memory usage to suit environments with limited VRAM.
    /// </summary>
    [JsonPropertyName("low_vram")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool LowVram { get; set; }

    /// <summary>
    /// Use half-precision (FP16) for storing key-value pairs. When enabled, the model stores key-value pairs in 16-bit floating-point format to reduce memory usage.
    /// </summary>
    [JsonPropertyName("f16_kv")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool F16Kv { get; set; }

    /// <summary>
    /// //TODO Comments
    /// </summary>
    [JsonPropertyName("logits_all")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool LogitsAll { get; set; }

    /// <summary>
    /// Vocabulary-only mode. When enabled, the model will generate only words from the vocabulary, suitable for specific tasks.
    /// </summary>
    [JsonPropertyName("vocab_only")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool VocabOnly { get; set; }

    /// <summary>
    /// Use memory-mapped files. When enabled, the model utilizes memory-mapped files to optimize loading and running large models.
    /// </summary>
    [JsonPropertyName("use_mmap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool UseMmap { get; set; }

    /// <summary>
    /// Use memory locking. When enabled, the model locks memory to prevent data from being swapped to disk, enhancing performance.
    /// </summary>
    [JsonPropertyName("use_mlock")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool UseMlock { get; set; }

    /// <summary>
    /// //TODO Comments
    /// </summary>
    [JsonPropertyName("embedding_only")]
    public bool EmbeddingOnly { get; set; }

    /// <summary>
    /// Number of threads. This parameter specifies the number of threads used for model computation to optimize parallel processing capabilities.
    /// </summary>
    [JsonPropertyName("num_thread")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int NumThread { get; set; }

    #endregion

    #region Runtime Options

    /// <summary>
    /// The number of generated sequences to keep. This parameter determines how many of the top output sequences are retained during text generation.
    /// </summary>
    [JsonPropertyName("num_keep")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int NumKeep { get; set; }

    /// <summary>
    /// Sets the random number seed to use for generation. <br />
    /// Setting this to a specific number will make the model generate the same text for the same prompt. (Default: 0). <br />
    /// For reproducible outputs, set temperature to 0 and seed to a number.
    /// </summary>
    [JsonPropertyName("seed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long Seed { get; set; } = 0;

    /// <summary>
    /// Maximum number of tokens to predict when generating text. (Default: 128, -1 = infinite generation, -2 = fill context).
    /// </summary>
    [JsonPropertyName("num_predict")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int NumPredict { get; set; } = 128;

    /// <summary>
    /// Reduces the probability of generating nonsense.
    /// A higher value(e.g. 100) will give more diverse answers, while a lower value(e.g. 10) will be more conservative. (Default: 40).
    /// </summary>
    [JsonPropertyName("top_k")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int TopK { get; set; } = 40;

    /// <summary>
    /// Works together with top-k.
    /// A higher value(e.g., 0.95) will lead to more diverse text, while a lower value(e.g., 0.5) will generate more focused and conservative text. (Default: 0.9).
    /// </summary>
    [JsonPropertyName("top_p")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double TopP { get; set; } = 0.9;

    /// <summary>
    /// Tail free sampling is used to reduce the impact of less probable tokens from the output.
    /// A higher value (e.g., 2.0) will reduce the impact more, while a value of 1.0 disables this setting. (default: 1).
    /// </summary>
    [JsonPropertyName("tfs_z")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double TfsZ { get; set; } = 1.0;

    /// <summary>
    /// Typical sampling probability. This parameter controls the diversity of text generation by selecting words with typical probabilities.
    /// </summary>
    [JsonPropertyName("typical_p")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double TypicalP { get; set; }

    /// <summary>
    /// Sets how far back for the model to look back to prevent repetition. (Default: 64, 0 = disabled, -1 = num_ctx).
    /// </summary>
    [JsonPropertyName("repeat_last_n")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int RepeatLastN { get; set; } = 64;

    /// <summary>
    /// The temperature of the model.Increasing the temperature will make the model answer more creatively. (Default: 0.8).
    /// </summary>
    [JsonPropertyName("temperature")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Temperature { get; set; } = 0.8;

    /// <summary>
    /// Sets how strongly to penalize repetitions.
    /// A higher value(e.g., 1.5) will penalize repetitions more strongly, while a lower value(e.g., 0.9) will be more lenient. (Default: 1.1).
    /// </summary>
    [JsonPropertyName("repeat_penalty")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double RepeatPenalty { get; set; } = 1.1;

    /// <summary>
    /// Presence penalty coefficient. Used to reduce the generation of repeated words; higher values penalize words that have already appeared.
    /// </summary>
    [JsonPropertyName("presence_penalty")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double PresencePenalty { get; set; }

    /// <summary>
    /// Frequency penalty coefficient. Used to reduce the generation of high-frequency words; higher values penalize frequently occurring words.
    /// </summary>
    [JsonPropertyName("frequency_penalty")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double FrequencyPenalty { get; set; }

    /// <summary>
    /// Enable Mirostat sampling for controlling perplexity. (default: 0, 0 = disabled, 1 = Mirostat, 2 = Mirostat 2.0).
    /// </summary>
    [JsonPropertyName("mirostat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Mirostat { get; set; }

    /// <summary>
    /// Controls the balance between coherence and diversity of the output.
    /// A lower value will result in more focused and coherent text. (Default: 5.0).
    /// </summary>
    [JsonPropertyName("mirostat_tau")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double MirostatTau { get; set; } = 5.0;

    /// <summary>
    /// Influences how quickly the algorithm responds to feedback from the generated text.
    /// A lower learning rate will result in slower adjustments, while a higher learning rate will make the algorithm more responsive. (Default: 0.1).
    /// </summary>
    [JsonPropertyName("mirostat_eta")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double MirostatEta { get; set; } = 0.1;

    /// <summary>
    /// Newline penalty. This parameter decides whether to penalize the generation of new newline characters to control the format of the output text.
    /// </summary>
    [JsonPropertyName("penalize_newline")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool PenalizeNewline { get; set; }

    /// <summary>
    /// Sets the stop sequences to use.When this pattern is encountered the LLM will stop generating text and return.
    /// Multiple stop patterns may be set by specifying multiple separate stop parameters in a modelfile.
    /// </summary>
    [JsonPropertyName("stop")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Stop { get; set; }

    #endregion
}

namespace Ollama.Core.Models.Base;

public abstract class CompletionDoneResponseBase
{
    /// <summary>
    /// The model name.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; protected set; } = null!;

    /// <summary>
    /// Gets the timestamp associated with generation activity for this completions response.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; protected set; }

    /// <summary>
    /// Gets the done status.
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; protected set; }

    /// <summary>
    /// An encoding of the conversation used in this response, this can be sent in the next request to keep a conversational memory
    /// </summary>
    [JsonPropertyName("context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
    public long[]? Context { get; protected set; }

    /// <summary>
    /// Time spent generating the response in nanoseconds.
    /// </summary>
    [JsonPropertyName("total_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long TotalDuration { get; protected set; }
    /// <summary>
    /// Time spent in nanoseconds loading the model in nanoseconds.
    /// </summary>
    [JsonPropertyName("load_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long LoadDuration { get; protected set; }
    /// <summary>
    /// Number of tokens in the prompt.
    /// </summary>
    [JsonPropertyName("prompt_eval_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int PromptEvalCount { get; protected set; }
    /// <summary>
    /// Time spent in nanoseconds evaluating the prompt in nanoseconds.
    /// </summary>
    [JsonPropertyName("prompt_eval_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long PromptEvalDuration { get; protected set; }
    /// <summary>
    /// Number of tokens in the response.<br />
    /// To calculate how fast the response is generated in tokens per second (token/s), divide eval_count / eval_duration * 10^9.
    /// </summary>
    [JsonPropertyName("eval_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int EvalCount { get; protected set; }
    /// <summary>
    /// Time in nanoseconds spent generating the response in nanoseconds.
    /// </summary>
    [JsonPropertyName("eval_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long EvalDuration { get; protected set; }
}

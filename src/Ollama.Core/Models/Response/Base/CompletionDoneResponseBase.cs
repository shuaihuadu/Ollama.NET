namespace Ollama.Core.Models.Base;

/// <summary>
/// The completion done response base.
/// </summary>
public abstract class CompletionDoneResponseBase : CompletionResponseBase
{
    /// <summary>
    /// The reason the model stopped generating text.
    /// </summary>
    [JsonPropertyName("done_reason")]
    public string? DoneReason { get; set; }

    /// <summary>
    /// Time spent generating the response in nanoseconds.
    /// </summary>
    [JsonPropertyName("total_duration")]
    public long TotalDuration { get; set; }

    /// <summary>
    /// Time spent in nanoseconds loading the model in nanoseconds.
    /// </summary>
    [JsonPropertyName("load_duration")]
    public long LoadDuration { get; set; }

    /// <summary>
    /// Number of tokens in the prompt.
    /// </summary>
    [JsonPropertyName("prompt_eval_count")]
    public int PromptEvalCount { get; set; }

    /// <summary>
    /// Time spent in nanoseconds evaluating the prompt in nanoseconds.
    /// </summary>
    [JsonPropertyName("prompt_eval_duration")]
    public long PromptEvalDuration { get; set; }

    /// <summary>
    /// Number of tokens in the response.<br />
    /// To calculate how fast the response is generated in tokens per second (token/s), divide eval_count / eval_duration * 10^9.
    /// </summary>
    [JsonPropertyName("eval_count")]
    public int EvalCount { get; set; }

    /// <summary>
    /// Time in nanoseconds spent generating the response in nanoseconds.
    /// </summary>
    [JsonPropertyName("eval_duration")]
    public long EvalDuration { get; set; }
}

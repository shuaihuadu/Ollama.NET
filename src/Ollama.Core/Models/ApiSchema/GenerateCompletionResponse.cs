namespace Ollama.Core.Models;

public class GenerateCompletionResponse
{
    [JsonConstructor]
    internal GenerateCompletionResponse(
        string model,
        DateTimeOffset createdAt,
        string response,
        bool done,
        long[] context,
        long totalDuration,
        long loadDuration,
        int promptEvalCount,
        long promptEvalDuration,
        int evalCount,
        long evalDuration)
    {
        this.Model = model;
        this.CreatedAt = createdAt;
        this.Response = response;
        this.Done = done;
        this.Context = context;
        this.TotalDuration = totalDuration;
        this.LoadDuration = loadDuration;
        this.PromptEvalCount = promptEvalCount;
        this.PromptEvalDuration = promptEvalDuration;
        this.EvalCount = evalCount;
        this.EvalDuration = evalDuration;
    }

    /// <summary>
    /// The model name.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; }

    /// <summary>
    /// Gets the timestamp associated with generation activity for this completions response.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; }

    /// <summary>
    /// Gets the done status.
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; }

    /// <summary>
    /// An encoding of the conversation used in this response, this can be sent in the next request to keep a conversational memory
    /// </summary>
    [JsonPropertyName("context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
    public long[] Context { get; }

    /// <summary>
    /// Time spent generating the response in nanoseconds.
    /// </summary>
    [JsonPropertyName("total_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long TotalDuration { get; }
    /// <summary>
    /// Time spent in nanoseconds loading the model in nanoseconds.
    /// </summary>
    [JsonPropertyName("load_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long LoadDuration { get; }
    /// <summary>
    /// Number of tokens in the prompt.
    /// </summary>
    [JsonPropertyName("prompt_eval_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int PromptEvalCount { get; }
    /// <summary>
    /// Time spent in nanoseconds evaluating the prompt in nanoseconds.
    /// </summary>
    [JsonPropertyName("prompt_eval_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long PromptEvalDuration { get; }
    /// <summary>
    /// Number of tokens in the response.<br />
    /// To calculate how fast the response is generated in tokens per second (token/s), divide eval_count / eval_duration * 10^9.
    /// </summary>
    [JsonPropertyName("eval_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int EvalCount { get; }
    /// <summary>
    /// Time in nanoseconds spent generating the response in nanoseconds.
    /// </summary>
    [JsonPropertyName("eval_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long EvalDuration { get; }
}

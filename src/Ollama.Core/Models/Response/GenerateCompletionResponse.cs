namespace Ollama.Core.Models;

public class GenerateCompletionResponse : CompletionDoneResponseBase
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
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; }
}

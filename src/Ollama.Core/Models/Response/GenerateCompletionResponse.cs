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
        Model = model;
        CreatedAt = createdAt;
        Response = response;
        Done = done;
        Context = context;
        TotalDuration = totalDuration;
        LoadDuration = loadDuration;
        PromptEvalCount = promptEvalCount;
        PromptEvalDuration = promptEvalDuration;
        EvalCount = evalCount;
        EvalDuration = evalDuration;
    }

    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; }

    /// <summary>
    /// An encoding of the conversation used in this response, this can be sent in the next request to keep a conversational memory
    /// </summary>
    [JsonPropertyName("context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
    public long[]? Context { get; protected set; }
}

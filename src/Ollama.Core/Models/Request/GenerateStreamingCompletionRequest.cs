namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion"/>
/// </summary>
public class GenerateStreamingCompletionRequest : GenerateCompletionRequestBase
{
    [JsonPropertyName("stream")]
    public override bool Stream => true;
}
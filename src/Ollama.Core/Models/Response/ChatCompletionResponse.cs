namespace Ollama.Core.Models;

/// <summary>
/// https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion
/// </summary>
public class ChatCompletionResponse : CompletionDoneResponseBase
{
    /// <summary>
    /// The messages of the chat, this can be used to keep a chat memory
    /// </summary>
    [JsonPropertyName("message")]
    public ChatMessage? Message { get; set; }
}
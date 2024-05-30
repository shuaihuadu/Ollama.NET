﻿namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion"/>
/// </summary>
public class ChatCompletion : CompletionDoneResponseBase
{
    /// <summary>
    /// The messages of the chat, this can be used to keep a chat memory
    /// </summary>
    [JsonPropertyName("message")]
    public ChatMessage? Message { get; set; }
}
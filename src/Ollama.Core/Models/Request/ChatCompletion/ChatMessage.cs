namespace Ollama.Core.Models;

/// <summary>
/// The messages of the chat, this can be used to keep a chat memory
/// </summary>
public class ChatMessage
{
    public ChatMessage() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatMessage"/> with role and content.
    /// </summary>
    /// <param name="role">Role of the chat of the message.</param>
    /// <param name="content">The chat message content.</param>
    public ChatMessage(ChatMessageRole role, string content)
    {
        this.Role = role;
        this.Content = content;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatMessage"/> with role,content and images.
    /// </summary>
    /// <param name="role">Role of the chat of the message.</param>
    /// <param name="content">The chat message content.</param>
    /// <param name="images">A list of base64-encoded images(for multimodal models such as llava).</param>
    public ChatMessage(ChatMessageRole role, string content, List<string> images)
        : this(role, content)
    {
        Images = images;
    }

    /// <summary>
    /// Role of the chat of the message.
    /// </summary>
    [JsonPropertyName("role")]
    [JsonConverter(typeof(ChatMessageRoleConverter))]
    public ChatMessageRole? Role { get; set; }

    /// <summary>
    /// The chat message content.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// A list of base64-encoded images(for multimodal models such as llava).
    /// </summary>
    [JsonPropertyName("images")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Images { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.Content ?? string.Empty;
    }
}

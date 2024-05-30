namespace Ollama.Core.Models;

public class ChatMessageHistory : IList<ChatMessage>, IReadOnlyList<ChatMessage>
{
    private readonly List<ChatMessage> _messages;

    /// <summary>
    /// Initialize a new instance of the <see cref="ChatMessageHistory"/> with empty history.
    /// </summary>
    public ChatMessageHistory()
    {
        this._messages = [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatMessageHistory"/> with a system message.
    /// </summary>
    /// <param name="systemMessage">The system message to add to the history.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="systemMessage"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="systemMessage"/> is empty or whitespace.
    /// </exception>
    public ChatMessageHistory(string systemMessage)
    {
        Argument.AssertNotNullOrWhiteSpace(systemMessage, nameof(systemMessage));

        this._messages = [];
        this.AddSystemMessage(systemMessage);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatMessageHistory"/> with the history messages.
    /// </summary>
    /// <param name="messages">The messages to copy into the history.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="messages"/> is null.
    /// </exception>
    public ChatMessageHistory(IEnumerable<ChatMessage> messages)
    {
        Argument.AssertNotNull(messages, nameof(messages));

        this._messages = new(messages);
    }

    /// <summary>
    /// Add an assistant message to the chat message history.
    /// </summary>
    /// <param name="content">Message content</param>
    public void AddAssistantMessage(string content) => this.AddMessage(ChatMessageRole.Assistant, content);

    /// <summary>
    /// Add a system message to the chat message history
    /// </summary>
    /// <param name="content">Message content.</param>
    public void AddSystemMessage(string systemMessage) => this.AddMessage(ChatMessageRole.System, systemMessage);

    /// <summary>
    /// Add a user message to the chat message history.
    /// </summary>
    /// <param name="content">Message content.</param>
    public void AddUserMessage(string conetent) => this.AddMessage(ChatMessageRole.User, conetent);

    /// <summary>
    /// Add a <see cref="ChatMessage"/> to the message history.
    /// </summary>
    /// <param name="role">Role of the message.</param>
    /// <param name="content">Message content.</param>
    public void AddMessage(ChatMessageRole role, string content) => this.Add(new(role, content));

    #region IList<ChatMessage>, IReadOnlyList<ChatMessage> Implementations

    /// <inheritdoc />
    public ChatMessage this[int index]
    {
        get => this._messages[index];
        set
        {
            Argument.AssertNotNull(value, nameof(value));

            this._messages[index] = value;
        }
    }

    /// <inheritdoc />
    public int Count => this._messages.Count;

    /// <inheritdoc />
    bool ICollection<ChatMessage>.IsReadOnly => false;

    /// <inheritdoc />
    public void Add(ChatMessage item)
    {
        Argument.AssertNotNull(item, nameof(item));

        this._messages.Add(item);
    }

    /// <inheritdoc />
    public void Clear() => this._messages.Clear();

    /// <inheritdoc />
    public bool Contains(ChatMessage item)
    {
        Argument.AssertNotNull(item, nameof(item));

        return this._messages.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(ChatMessage[] array, int arrayIndex) => this._messages.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public int IndexOf(ChatMessage item)
    {
        Argument.AssertNotNull(item, nameof(item));

        return this._messages.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, ChatMessage item)
    {
        Argument.AssertNotNull(item, nameof(item));

        this._messages.Insert(index, item);
    }

    /// <inheritdoc />
    public bool Remove(ChatMessage item)
    {
        Argument.AssertNotNull(item, nameof(item));

        return this._messages.Remove(item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index) => this._messages.RemoveAt(index);

    /// <inheritdoc />
    IEnumerator<ChatMessage> IEnumerable<ChatMessage>.GetEnumerator() => this._messages.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => this._messages.GetEnumerator();

    #endregion
}
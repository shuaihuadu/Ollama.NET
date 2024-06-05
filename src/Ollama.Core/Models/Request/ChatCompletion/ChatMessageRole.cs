namespace Ollama.Core.Models;

/// <summary>
/// A description of the intended purpose of a message within a chat completions interaction.
/// </summary>
public readonly struct ChatMessageRole : IEquatable<ChatMessageRole>
{
    /// <summary>
    /// The role that instructs or sets the behavior of the assistant.
    /// </summary>
    public static ChatMessageRole System { get; } = new("system");

    /// <summary>
    /// The role that provides responses to system-instructed, user-prompted input.
    /// </summary>
    public static ChatMessageRole Assistant { get; } = new("assistant");

    /// <summary>
    /// The role that provides input for chat completions.
    /// </summary>
    public static ChatMessageRole User { get; } = new("user");

    /// <summary>
    /// Gets the label associated with this ChatMessageRole.
    /// </summary>
    /// <remarks>
    /// The label is what will be serialized into the "role" message field of the Chat Message format.
    /// </remarks>
    public string Label { get; }

    /// <summary>
    /// Creates a new ChatMessageRole instance with the provided label.
    /// </summary>
    /// <param name="label">The label to associate with this ChatMessageRole.</param>
    [JsonConstructor]
    public ChatMessageRole(string label)
    {
        Argument.AssertNotNullOrWhiteSpace(label, nameof(label));

        this.Label = label;
    }

    /// <summary>
    /// Returns a value indicating whether two ChatMessageRole instances are equivalent, as determined by a
    /// case-insensitive comparison of their labels.
    /// </summary>
    /// <param name="left"> the first ChatMessageRole instance to compare </param>
    /// <param name="right"> the second ChatMessageRole instance to compare </param>
    /// <returns> true if left and right are both null or have equivalent labels; false otherwise </returns>
    public static bool operator ==(ChatMessageRole left, ChatMessageRole right) => left.Equals(right);

    /// <summary>
    /// Returns a value indicating whether two ChatMessageRole instances are not equivalent, as determined by a
    /// case-insensitive comparison of their labels.
    /// </summary>
    /// <param name="left"> the first ChatMessageRole instance to compare </param>
    /// <param name="right"> the second ChatMessageRole instance to compare </param>
    /// <returns> false if left and right are both null or have equivalent labels; true otherwise </returns>
    public static bool operator !=(ChatMessageRole left, ChatMessageRole right) => !(left == right);

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is ChatMessageRole otherRole && this == otherRole;

    /// <inheritdoc/>
    public bool Equals(ChatMessageRole other) => string.Equals(this.Label, other.Label, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(this.Label ?? string.Empty);

    /// <inheritdoc/>
    public override string ToString() => this.Label ?? string.Empty;
}


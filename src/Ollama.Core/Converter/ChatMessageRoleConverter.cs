namespace Ollama.Core.Converter;

/// <summary>
/// The chat message role converter, between <see cref="ChatMessageRole"/> and string value.
/// </summary>
internal sealed class ChatMessageRoleConverter : JsonConverter<ChatMessageRole?>
{
    public override ChatMessageRole? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? role = reader.GetString();

        if (role is null)
        {
            return null;
        }

        if (role.Equals("user", StringComparison.OrdinalIgnoreCase))
        {
            return ChatMessageRole.User;
        }

        if (role.Equals("assistant", StringComparison.OrdinalIgnoreCase))
        {
            return ChatMessageRole.Assistant;
        }

        if (role.Equals("system", StringComparison.OrdinalIgnoreCase))
        {
            return ChatMessageRole.System;
        }

        throw new JsonException($"Unexpected author role: {role}");
    }

    public override void Write(Utf8JsonWriter writer, ChatMessageRole? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        if (value == ChatMessageRole.User)
        {
            writer.WriteStringValue(ChatMessageRole.User.Label);
        }
        else if (value == ChatMessageRole.Assistant)
        {
            writer.WriteStringValue(ChatMessageRole.Assistant.Label);
        }
        else if (value == ChatMessageRole.System)
        {
            writer.WriteStringValue(ChatMessageRole.System.Label);
        }
        else
        {
            throw new JsonException($"Ollama API doesn't support author role: {value}");
        }
    }
}

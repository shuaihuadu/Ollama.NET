namespace Ollama.Core.Tests;

public static class StringExtensions
{
    /// <summary>
    /// Checks the <paramref name="json"/> is a valid json.
    /// </summary>
    /// <param name="json">The json string.</param>
    /// <returns></returns>
    public static bool IsValidJson(this string json)
    {
        try
        {
            JsonDocument.Parse(json);

            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
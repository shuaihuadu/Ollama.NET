namespace Ollama.Core.Models;

public class CreateModel
{
    [JsonConstructor]
    internal CreateModel(string status)
    {
        this.Status = status;
    }

    /// <summary>
    /// Gets the content fragment associated with this update.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; }
}

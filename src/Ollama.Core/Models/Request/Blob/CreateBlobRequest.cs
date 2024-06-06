namespace Ollama.Core.Models;

/// <summary>
/// https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model
/// </summary>
internal sealed class CreateBlobRequest
{
    /// <summary>
    /// The SHA256 digest of the blob.
    /// </summary>
    [JsonIgnore]
    public string Digest { get; set; } = null!;

    /// <summary>
    /// The file content to create a blob.
    /// </summary>
    [JsonIgnore]
    public byte[] Content { get; set; } = null!;

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest($"api/blobs/{this.Digest}");
    }
}
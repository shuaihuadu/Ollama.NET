namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model"/>
/// </summary>
internal sealed class CreateBlobRequest
{
    /// <summary>
    /// The SHA256 digest of the blob.
    /// </summary>
    [JsonIgnore]
    public required string Digest { get; set; }

    /// <summary>
    /// The file content to create a blob.
    /// </summary>
    [JsonIgnore]
    public required byte[] Content { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreatePostRequest($"api/blobs/{this.Digest}");
    }
}
namespace Ollama.Core.Models;

/// <summary>
/// <see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model"/>
/// </summary>
internal sealed class CheckBlobExistsRequest
{
    /// <summary>
    /// The SHA256 digest of the blob
    /// </summary>
    [JsonIgnore]
    public string Digest { get; set; }

    /// <summary>
    /// To the <see cref="HttpRequestMessage"/>  for send a http request.
    /// </summary>
    /// <returns></returns>
    public HttpRequestMessage ToHttpRequestMessage()
    {
        return HttpRequest.CreateHeadRequest($"api/blobs/{this.Digest}");
    }
}
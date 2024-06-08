namespace Ollama.Core.Internal;

internal static class Verify
{
    internal static void ValidateHttpClientAndEndpoint(HttpClient? httpClient, Uri? endpoint)
    {
        string message = $"The {nameof(httpClient)}.{nameof(HttpClient.BaseAddress)} and {nameof(endpoint)} are both null or empty. Please ensure at least one is provided.";

        if (string.IsNullOrEmpty(httpClient?.BaseAddress?.AbsoluteUri) && endpoint is null && string.IsNullOrEmpty(endpoint?.AbsoluteUri))
        {
            throw new ArgumentException(message);
        }
    }

    internal static void ValidateHttpClientAndEndpoint(HttpClient? httpClient, string? endpoint)
    {
        Uri? endpointUri = null;
        try
        {
            ValidateUrl(endpoint);

            endpointUri = new Uri(endpoint!);
        }
        catch (ArgumentException)
        {
            endpoint = null;
        }

        ValidateHttpClientAndEndpoint(httpClient, endpointUri);
    }

    internal static void ValidateUrl(string? url, bool allowQuery = false, string paramName = "url")
    {
        Argument.AssertNotNull(url, nameof(url));
        Argument.AssertNotNullOrEmpty(url!, paramName ?? nameof(url));

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || string.IsNullOrEmpty(uri.Host))
        {
            throw new ArgumentException($"The `{url}` is not valid.", paramName);
        }

        if (!allowQuery && !string.IsNullOrEmpty(uri.Query))
        {
            throw new ArgumentException($"The `{url}` is not valid: it cannot contain query parameters.", paramName);
        }

        if (!string.IsNullOrEmpty(uri.Fragment))
        {
            throw new ArgumentException($"The `{url}` is not valid: it cannot contain URL fragments.", paramName);
        }
    }
}

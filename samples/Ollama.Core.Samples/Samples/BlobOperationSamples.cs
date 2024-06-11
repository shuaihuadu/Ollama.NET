namespace Ollama.Core.Samples;

public class BlobOperationSamples : OllamaClientSampleBase
{
    public static async Task CheckBlobExists(string digest)
    {
        OllamaClient client = GetTestClient();

        bool result = await client.CheckBlobExistsAsync(digest);

        Console.WriteLine(result);
    }


    public static async Task CreateBlob()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Resources", "ollama-openapi.yaml");

        string digest = "sha256:fa304d6750612c207b8705aca35391761f29492534e90b30575e4980d6ca82f6";

        OllamaClient client = GetTestClient();

        byte[] content = await File.ReadAllBytesAsync(filePath);

        await client.CreateBlobAsync(digest, content);
    }
}
namespace Ollama.Core.Samples;

public class GenerateEmbeddingSamples : OllamaClientSampleBase
{
    public static async Task GenerateEmbedding()
    {
        OllamaClient client = GetTestClient();

        EmbeddingResponse response = await client.GenerateEmbeddingAsync("all-minilm", "Hello Embedding!");

        Console.WriteLine(response.AsJson());
        Console.WriteLine(response.Embedding.Length);
    }
}
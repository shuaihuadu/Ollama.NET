namespace Ollama.Core.Samples;

public class GenerateEmbeddingSamples : OllamaClientSampleBase
{
    public async Task GenerateEmbedding()
    {
        using OllamaClient client = GetTestClient();

        EmbeddingResponse response = await client.GenerateEmbeddingAsync("all-minilm", "Hello Embedding!");

        Console.WriteLine(response.Embedding.AsJson());
        Console.WriteLine(response.Embedding.Length);
    }
}
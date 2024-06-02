namespace Ollama.Core.Tests;

public class GenerateEmbeddingTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    [Fact]
    public async Task GenerateEmbedding()
    {
        OllamaClient client = GetTestClient();

        EmbeddingResponse response = await client.GenerateEmbeddingAsync("all-minilm", "Hello Embedding!");

        Assert.NotNull(response);
        Assert.NotNull(response.Embedding);
        Assert.True(response.Embedding.HasValue);
        Assert.Equal(384, response.Embedding.Value.Length);
    }
}
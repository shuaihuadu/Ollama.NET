namespace Ollama.Core.Tests;

public class GenerateEmbeddingTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    [Fact]
    public async Task GenerateEmbedding()
    {
        OllamaClient client = GetTestClient();

        EmbeddingResponse response = await client.GenerateEmbeddingAsync("all-minilm", "Hello Embedding!");

        Assert.NotNull(response);
        Assert.False(response.Embedding.IsEmpty);
        Assert.Equal(384, response.Embedding.Length);
    }
}
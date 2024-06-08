namespace Ollama.Core.Tests.IntegrationTests;

public class GenerateEmbeddingTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    [Fact]
    public async Task GenerateEmbedding()
    {
        using OllamaClient client = GetTestClient();

        EmbeddingResponse response = await client.GenerateEmbeddingAsync("all-minilm", "Hello Embedding!");

        Assert.NotNull(response);
        Assert.False(response.Embedding.IsEmpty);
        Assert.Equal(384, response.Embedding.Length);
    }
}
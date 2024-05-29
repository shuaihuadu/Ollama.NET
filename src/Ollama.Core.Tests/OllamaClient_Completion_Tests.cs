namespace Ollama.Core.Tests;

public class OllamaClient_Test(ITestOutputHelper output) : BaseTest(output)
{
    [Fact]
    public async Task GenerateCompletionAsync_Test()
    {
        OllamaClient client = new("http://localhost:19888", LoggerFactory);

        //await client.GenerateCompletionAsync("qwen", "Hello!");

        StreamingResponse<GenerateCompletionResponse> response = await client.GenerateStreamingCompletionAsync("qwen", "Hello!");
        await foreach (GenerateCompletionResponse item in response)
        {
            Console.WriteLine(item.AsJson());
        }

        await client.GenerateCompletionAsync("qwen", "ÄãºÃÑ½!");
    }
}
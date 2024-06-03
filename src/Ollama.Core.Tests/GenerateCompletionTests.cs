namespace Ollama.Core.Tests;

public class GenerateCompletionTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string model = "llama3";

    [Fact]
    public async Task GenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(model, "Hello!");

        Assert.NotEmpty(response.Response);

        Asserts(response);
    }

    [Fact]
    public async Task GenerateCompletionStreaming()
    {
        OllamaClient client = GetTestClient();

        StreamingResponse<GenerateCompletionResponse> response = await client.GenerateCompletionStreamingAsync(model, "Hello!");

        Assert.NotNull(response);

        await foreach (GenerateCompletionResponse item in response)
        {
            Assert.NotEmpty(item.Model);
            Assert.Equal(model, item.Model);
            Assert.True(item.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));

            //Console.WriteLine(item.Response);

            if (item.Done)
            {
                Asserts(item);
            }
        }
    }

    [Fact]
    public async Task GenerateCompletion_FormatJson()
    {
        OllamaClient client = GetTestClient();

        GenerateCompletionOptions options = new()
        {
            Model = model,
            Prompt = "What color is the sky at different times of the day? Respond using JSON.",
            Format = "json"
        };

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(options);

        Assert.True(response.Response.IsValidJson());
    }

    private static void Asserts(GenerateCompletionResponse response)
    {
        Assert.NotEmpty(response.Model);
        Assert.Equal(model, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.True(response.Done);
        Assert.Equal("stop", response.DoneReason, ignoreCase: true);
        Assert.NotNull(response.Context);
        Assert.True(response.Context.Length > 0);
        Assert.True(response.TotalDuration > 0);
        Assert.True(response.LoadDuration > 0);
        //Assert.True(response.PromptEvalCount > 0);
        //Assert.Equal(0, response.PromptEvalCount);
        Assert.True(response.PromptEvalDuration > 0);
        Assert.True(response.EvalCount > 0);
        Assert.True(response.EvalDuration > 0);
    }
}
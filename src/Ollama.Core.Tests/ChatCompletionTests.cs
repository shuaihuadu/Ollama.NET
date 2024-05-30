namespace Ollama.Core.Tests;

public class ChatCompletionTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string model = "llama3";

    [Fact]
    public async Task GenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello!");

        ChatCompletion response = await client.ChatCompletionAsync(model, messages);

        Assert.NotNull(response.Message);
    }


    private static void Asserts(GenerateCompletion response)
    {
        Assert.NotEmpty(response.Model);
        Assert.Equal(model, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.True(response.Done);
        Assert.NotNull(response.Context);
        Assert.True(response.Context.Length > 0);
        Assert.True(response.TotalDuration > 0);
        Assert.True(response.LoadDuration > 0);
        Assert.True(response.PromptEvalCount > 0);
        Assert.True(response.PromptEvalDuration > 0);
        Assert.True(response.EvalCount > 0);
        Assert.True(response.EvalDuration > 0);
    }
}
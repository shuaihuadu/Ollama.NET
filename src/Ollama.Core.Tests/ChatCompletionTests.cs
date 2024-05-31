namespace Ollama.Core.Tests;

public class ChatCompletionTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string model = "llama3";

    [Fact]
    public async Task ChatCompletion()
    {
        OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello!");

        ChatCompletion response = await client.ChatCompletionAsync(model, messages);

        Assert.NotNull(response.Message);
        Assert.NotNull(response.Message.Content);
        Assert.NotEmpty(response.Message.Content);
        Assert.Equal("stop", response.DoneReason, ignoreCase: true);

        Asserts(response);
    }

    [Fact]
    public async Task ChatCompletionWithChatMessageHistory()
    {
        OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello! What's the weather today?");
        messages.AddAssistantMessage("It's rainy!");
        messages.AddUserMessage("What should I do when I go out?");

        ChatCompletion response = await client.ChatCompletionAsync(model, messages);

        Assert.NotNull(response.Message);
        Assert.NotNull(response.Message.Content);
        Assert.NotEmpty(response.Message.Content);
        Assert.Equal("stop", response.DoneReason, ignoreCase: true);

        Console.WriteLine(response.Message);

        Asserts(response);
    }

    private static void Asserts(ChatCompletion response)
    {
        Assert.NotEmpty(response.Model);
        Assert.Equal(model, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.True(response.Done);
        Assert.True(response.TotalDuration > 0);
        Assert.True(response.LoadDuration > 0);
        //Assert.True(response.PromptEvalCount > 0);
        //Assert.Equal(0, response.PromptEvalCount);
        Assert.True(response.PromptEvalDuration > 0);
        Assert.True(response.EvalCount > 0);
        Assert.True(response.EvalDuration > 0);
    }
}
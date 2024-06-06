namespace Ollama.Core.Tests;

public class ChatCompletionTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    [Fact]
    public async Task ChatCompletion()
    {
        using OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello!");

        ChatCompletionResponse response = await client.ChatCompletionAsync(llama3, messages);

        Assert.NotNull(response.Message);
        Assert.NotNull(response.Message.Content);
        Assert.NotEmpty(response.Message.Content);
        Assert.Equal("stop", response.DoneReason, ignoreCase: true);

        Asserts(response);
    }

    [Fact]
    public async Task ChatCompletionStreaming()
    {
        using OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello!");

        StreamingResponse<ChatCompletionResponse> response = await client.ChatCompletionStreamingAsync(llama3, messages);

        Assert.NotNull(response);

        await foreach (ChatCompletionResponse item in response)
        {
            Assert.NotNull(item.Model);
            Assert.NotEmpty(item.Model);
            Assert.Equal(llama3, item.Model);
            Assert.True(item.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));

            //Console.WriteLine(item.Response);

            if (item.Done)
            {
                Asserts(item);
            }
        }
    }

    [Fact]
    public async Task ChatCompletion_WithChatMessageHistory()
    {
        using OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello! What's the weather today?");
        messages.AddAssistantMessage("It's rainy!");
        messages.AddUserMessage("What should I do when I go out?");

        ChatCompletionResponse response = await client.ChatCompletionAsync(llama3, messages);

        Assert.NotNull(response.Message);
        Assert.NotNull(response.Message.Content);
        Assert.NotEmpty(response.Message.Content);
        Assert.Equal("stop", response.DoneReason, ignoreCase: true);

        Console.WriteLine(response.Message);

        Asserts(response);
    }

    [Fact]
    public async Task ChatCompletion_WithImages()
    {
        byte[] bytes = await File.ReadAllBytesAsync(Path.Combine(AppContext.BaseDirectory, "Resources", "sk.png"));

        using HttpClient httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(600)
        };

        using OllamaClient client = new(httpClient, Endpoint, LoggerFactory);

        ChatMessageHistory messages = [];

        messages.AddUserMessage("What is in this image", [Convert.ToBase64String(bytes)]);

        ChatCompletionResponse response = await client.ChatCompletionAsync(llava, messages);

        Assert.NotNull(response.Message);
        Assert.NotNull(response.Message.Content);
        Assert.NotEmpty(response.Message.Content);
        Assert.Equal("stop", response.DoneReason, ignoreCase: true);

        Console.WriteLine(response.Message);

        Asserts(response);
    }

    [Fact]
    public async Task ChatCompletion_WithOptions()
    {
        using OllamaClient client = GetTestClient();

        ParameterOptions parameterOptions = new()
        {
            Temperature = 0.8,
            RepeatPenalty = 1.2,
            PresencePenalty = 1.5,
            FrequencyPenalty = 1.0,
            Stop = ["stop_sequences"],
            NumCtx = 2048
        };

        ChatMessageHistory messages = [];

        messages.AddSystemMessage("You are a librarian, expert about books");

        messages.AddUserMessage("Hi, I'm looking for book suggestions");

        messages.AddAssistantMessage("Sure, I'd be happy to help! What kind of books are you interested in? Fiction or non-fiction? Any particular genre?");

        messages.AddUserMessage("User: I love history and philosophy, I'd like to learn something new about Greece, any suggestion?");


        ChatCompletionOptions options = new()
        {
            Model = llama3,
            Options = parameterOptions,
            Messages = messages
        };

        //ChatCompletionResponse response = await client.ChatCompletionAsync(options);
        ChatCompletionResponse response = await client.ChatCompletionAsync(llama3, messages);

        Console.WriteLine(response.Message);
    }

    private static void Asserts(ChatCompletionResponse response)
    {
        Assert.NotNull(response.Model);
        Assert.NotEmpty(response.Model);
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
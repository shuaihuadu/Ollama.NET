namespace Ollama.Core.Samples;

public class ChatCompletionSamples : OllamaClientSampleBase
{
    public static async Task ChatCompletion()
    {
        using OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello!");

        ChatCompletionResponse response = await client.ChatCompletionAsync(llama3, messages);

        Console.WriteLine(response.Message?.Content);
    }


    public static async Task ChatCompletionStreaming()
    {
        using OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello!");

        StreamingResponse<ChatCompletionResponse> response = await client.ChatCompletionStreamingAsync(llama3, messages);

        await foreach (ChatCompletionResponse item in response)
        {
            Console.WriteLine(item.Message?.Content);
        }
    }


    public static async Task ChatCompletionWithChatMessageHistory()
    {
        using OllamaClient client = GetTestClient();

        ChatMessageHistory messages = [];

        messages.AddUserMessage("Hello! What's the weather today?");
        messages.AddAssistantMessage("It's rainy!");
        messages.AddUserMessage("What should I do when I go out?");

        ChatCompletionResponse response = await client.ChatCompletionAsync(llama3, messages);

        Console.WriteLine(response.Message);
    }


    public static async Task ChatCompletionWithImages()
    {
        byte[] bytes = await File.ReadAllBytesAsync(Path.Combine(AppContext.BaseDirectory, "Resources", "sk.png"));

        using HttpClient httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(600)
        };

        using OllamaClient client = new(httpClient, Endpoint);

        ChatMessageHistory messages = [];

        messages.AddUserMessage("What is in this image", [Convert.ToBase64String(bytes)]);

        ChatCompletionResponse response = await client.ChatCompletionAsync(llava, messages);

        Console.WriteLine(response.Message);
    }


    public static async Task ChatCompletionWithOptions()
    {
        using OllamaClient client = GetTestClient();

        ParameterOptions parameterOptions = new()
        {
            NumKeep = 5,
            Seed = 42,
            NumPredict = 100,
            TopK = 20,
            TopP = 0.9,
            TfsZ = 0.5,
            TypicalP = 0.7,
            RepeatLastN = 33,
            Temperature = 0.8,
            RepeatPenalty = 1.2,
            PresencePenalty = 1.5,
            FrequencyPenalty = 1.0,
            Mirostat = 1,
            MirostatTau = 0.8,
            MirostatEta = 0.6,
            PenalizeNewline = true,
            Stop = ["\n", "user"],
            Numa = false,
            NumCtx = 2048,
            NumBatch = 2,
            NumGpu = 0,
            MainGpu = 0,
            LowVram = true,
            F16Kv = true,
            VocabOnly = true,
            UseMmap = true,
            UseMlock = true,
            NumThread = 8
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

        ChatCompletionResponse response = await client.ChatCompletionAsync(options);

        Console.WriteLine(response.Message?.Content);

        StreamingResponse<ChatCompletionResponse> streamingResponse = await client.ChatCompletionStreamingAsync(options);

        await foreach (var item in streamingResponse)
        {
            Console.WriteLine(response.Message?.Content);
        }

        Console.WriteLine(response.Message);
    }
}
namespace Ollama.Core.Tests;

public class ModelOperationTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string model = "llama3";

    [Fact]
    public async Task LoadModelUseGenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModel response = await client.LoadModelUseGenerateCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task LoadModelUseChatCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModel response = await client.LoadModelUseChatCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task UnloadModelUseGenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModel response = await client.UnloadModelUseGenerateCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task UnloadModelUseChatCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModel response = await client.UnloadModelUseChatCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task CreateModel()
    {
        OllamaClient client = GetTestClient();

        CreateModel response = await client.CreateModelAsync("llama3-mario1", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        Assert.NotNull(response);
        Assert.Equal("success", response.Status);
    }


    [Fact]
    public async Task CreateModelStreaming()
    {
        OllamaClient client = GetTestClient();

        StreamingResponse<CreateModel> response = await client.CreateModelStreamingAsync("llama3-mario2", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        Assert.NotNull(response);

        await foreach (var item in response)
        {
            Console.WriteLine(item.Status);
            Assert.NotEmpty(item.Status);
        }
    }
    private static void Asserts(LoadModel response)
    {
        Assert.NotEmpty(response.Model);
        Assert.Equal(model, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.Empty(response.Response);
        Assert.True(response.Done);
    }

}
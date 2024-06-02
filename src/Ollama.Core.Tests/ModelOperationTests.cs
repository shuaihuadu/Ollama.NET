namespace Ollama.Core.Tests;

public class ModelOperationTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string model = "llama3";

    [Fact]
    public async Task LoadModelUseGenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.LoadModelUseGenerateCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task LoadModelUseChatCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.LoadModelUseChatCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task UnloadModelUseGenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.UnloadModelUseGenerateCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task UnloadModelUseChatCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.UnloadModelUseChatCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task CreateModel()
    {
        OllamaClient client = GetTestClient();

        CreateModelResponse response = await client.CreateModelAsync("llama3-mario1", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        Assert.NotNull(response);
        Assert.Equal("success", response.Status);
    }


    [Fact]
    public async Task CreateModelStreaming()
    {
        OllamaClient client = GetTestClient();

        StreamingResponse<CreateModelResponse> response = await client.CreateModelStreamingAsync("llama3-mario2", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        Assert.NotNull(response);

        await foreach (var item in response)
        {
            Assert.NotEmpty(item.Status);
        }
    }

    [Fact]
    public async Task ListModels()
    {
        OllamaClient client = GetTestClient();

        ListModelResponse models = await client.ListModelsAsync();

        foreach (var model in models.Models)
        {
            Assert.NotEmpty(model.Name);
        }
    }

    [Fact]
    public async Task ShowModel()
    {
        OllamaClient client = GetTestClient();

        ShowModelResponse response = await client.ShowModelAsync(model);

        Assert.NotNull(response);
    }

    private static void Asserts(LoadModelResponse response)
    {
        Assert.NotEmpty(response.Model);
        Assert.Equal(model, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.Empty(response.Response);
        Assert.True(response.Done);
    }
}
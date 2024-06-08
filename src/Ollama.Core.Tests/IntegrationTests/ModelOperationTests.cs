namespace Ollama.Core.Tests.IntegrationTests;

public class ModelOperationTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string model = "llama3";

    [Fact]
    public async Task LoadModelUseGenerateCompletion()
    {
        using OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.LoadModelUseGenerateCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task LoadModelUseChatCompletion()
    {
        using OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.LoadModelUseChatCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task UnloadModelUseGenerateCompletion()
    {
        using OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.UnloadModelUseGenerateCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task UnloadModelUseChatCompletion()
    {
        using OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.UnloadModelUseChatCompletionEndpointAsync(model);

        Asserts(response);
    }

    [Fact]
    public async Task CreateModel()
    {
        using OllamaClient client = GetTestClient();

        CreateModelResponse response = await client.CreateModelAsync("llama3-shuaihua", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        Assert.NotNull(response);
        Assert.Equal("success", response.Status);
    }


    [Fact]
    public async Task CreateModelStreaming()
    {
        using OllamaClient client = GetTestClient();

        StreamingResponse<CreateModelResponse> response = await client.CreateModelStreamingAsync("llama3-mario2", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        Assert.NotNull(response);

        await foreach (var item in response)
        {
            Assert.NotNull(item.Status);
            Assert.NotEmpty(item.Status);
        }
    }

    [Fact]
    public async Task ListModels()
    {
        using OllamaClient client = GetTestClient();

        ListModelResponse models = await client.ListModelsAsync();

        foreach (var model in models.Models)
        {
            Assert.NotEmpty(model.Model);
            Assert.NotEmpty(model.Name);

            //Console.WriteLine(model.Model);
            //Console.WriteLine(model.Name);
        }
    }

    [Fact]
    public async Task ListRunningModels()
    {
        using OllamaClient client = GetTestClient();

        ListRunningModelResponse models = await client.ListRunningModelsAsync();

        foreach (var model in models.Models)
        {
            Assert.NotEmpty(model.Model);
            Assert.NotEmpty(model.Name);

            //Console.WriteLine(model.Model);
            //Console.WriteLine(model.Name);
        }
    }

    [Fact]
    public async Task ShowModel()
    {
        using OllamaClient client = GetTestClient();

        ShowModelResponse response = await client.ShowModelAsync(model);

        Assert.NotNull(response);
    }


    [Fact]
    public async Task CopyModel()
    {
        using OllamaClient client = GetTestClient();

        await client.CopyModelAsync("llama3", "llama3-mario1");

        ListModelResponse response = await client.ListModelsAsync();

        Assert.Contains(response.Models, x => x.Name == "llama3-mario1:latest");
    }

    [Fact]
    public async Task DeleteModel()
    {
        using OllamaClient client = GetTestClient();

        await client.DeleteModelAsync("llama3-mario1");

        ListModelResponse response = await client.ListModelsAsync();

        Assert.DoesNotContain(response.Models, x => x.Name == "llama3-mario1:latest");
    }

    [Fact]
    public async Task PullModel()
    {
        string modelName = "all-minilm";

        using OllamaClient client = GetTestClient();

        await client.DeleteModelAsync(modelName);

        PullModelResponse response = await client.PullModelAsync(modelName);

        Assert.NotNull(response);
        Assert.Equal("success", response.Status);

    }

    [Fact]
    public async Task PullModelStreaming()
    {
        string modelName = "all-minilm";

        using OllamaClient client = GetTestClient();

        await client.DeleteModelAsync(modelName);

        StreamingResponse<PullModelResponse> response = await client.PullModelStreamingAsync(modelName);

        await foreach (var item in response)
        {
            Assert.NotNull(item.Status);
            Assert.NotEmpty(item.Status);

            //Console.WriteLine(item.Status);

            Console.WriteLine($"{item.Completed / item.Total:P2}");
        }
    }

    [Fact]
    public async Task PushModel()
    {
        //https://github.com/ollama/ollama/blob/main/docs/import.md Importing (GGUF)

        // 1.Sign - up for ollama.ai
        //  https://www.ollama.ai/signup
        // 2.Create a new model
        //  https://www.ollama.ai/new
        // 3.Create the model locally with your username as the namespace
        //  ollama create <ollama-username>/<model-name> -f /path/to/Modelfile
        // 4.Sign in the ollama account -> Ollama keys -> Add Ollama Public Key
        // 5. open id_******.pub on your local, copy and past to Ollama key
        // 6. Push the model

        string name = "shuaihuadu/phi3:unittest";

        using OllamaClient client = GetTestClient();

        PushModelResponse response = await client.PushModelAsync(name);

        Assert.NotNull(response);
    }

    [Fact]
    public async Task PushModelStreaming()
    {
        string name = "shuaihuadu/phi3:unittest";

        using OllamaClient client = GetTestClient();

        await client.PushModelStreamingAsync(name);

        StreamingResponse<PushModelResponse> response = await client.PushModelStreamingAsync(name);

        Assert.NotNull(response);

        await foreach (var item in response)
        {
            Assert.NotNull(item.Status);
            Assert.NotEmpty(item.Status);

            Console.WriteLine(item.Status);
        }
    }

    private static void Asserts(LoadModelResponse response)
    {
        Assert.NotNull(response.Model);
        Assert.NotEmpty(response.Model);
        Assert.Equal(model, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.NotNull(response.Response);
        Assert.Empty(response.Response);
        Assert.True(response.Done);
    }
}
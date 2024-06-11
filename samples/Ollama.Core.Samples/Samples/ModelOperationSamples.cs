namespace Ollama.Core.Samples;

public class ModelOperationSamples : OllamaClientSampleBase
{
    const string model = "llama3";


    public static async Task LoadModelUseGenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.LoadModelUseGenerateCompletionEndpointAsync(model);

        Console.WriteLine(response.Model);
    }


    public static async Task LoadModelUseChatCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.LoadModelUseChatCompletionEndpointAsync(model);

        Console.WriteLine(response.Model);
    }


    public static async Task UnloadModelUseGenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.UnloadModelUseGenerateCompletionEndpointAsync(model);

        Console.WriteLine(response.Model);
    }


    public static async Task UnloadModelUseChatCompletion()
    {
        OllamaClient client = GetTestClient();

        LoadModelResponse response = await client.UnloadModelUseChatCompletionEndpointAsync(model);

        Console.WriteLine(response.Model);
    }


    public static async Task CreateModel()
    {
        OllamaClient client = GetTestClient();

        CreateModelResponse response = await client.CreateModelAsync("llama3-shuaihua", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        Console.WriteLine(response.Status);
    }



    public static async Task CreateModelStreaming()
    {
        OllamaClient client = GetTestClient();

        StreamingResponse<CreateModelResponse> response = await client.CreateModelStreamingAsync("llama3-mario2", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

        await foreach (var item in response)
        {
            Console.WriteLine(item.Status);
        }
    }


    public static async Task ListModels()
    {
        OllamaClient client = GetTestClient();

        ListModelResponse models = await client.ListModelsAsync();

        foreach (var model in models.Models)
        {
            Console.WriteLine(model.Model);
            Console.WriteLine(model.Name);
        }
    }


    public static async Task ListRunningModels()
    {
        OllamaClient client = GetTestClient();

        ListRunningModelResponse models = await client.ListRunningModelsAsync();

        foreach (var model in models.Models)
        {
            Console.WriteLine(model.Model);
            Console.WriteLine(model.Name);
        }
    }


    public static async Task ShowModel()
    {
        OllamaClient client = GetTestClient();

        ShowModelResponse response = await client.ShowModelAsync(model);

        Console.WriteLine(response.AsJson());
    }



    public static async Task CopyModel()
    {
        OllamaClient client = GetTestClient();

        await client.CopyModelAsync("llama3", "llama3-mario1");

        ListModelResponse response = await client.ListModelsAsync();

        Console.WriteLine(response.AsJson());
    }


    public static async Task DeleteModel()
    {
        OllamaClient client = GetTestClient();

        await client.DeleteModelAsync("llama3-mario1");

        ListModelResponse response = await client.ListModelsAsync();

        Console.WriteLine(response.AsJson());
    }


    public static async Task PullModel()
    {
        string modelName = "all-minilm";

        OllamaClient client = GetTestClient();

        await client.DeleteModelAsync(modelName);

        PullModelResponse response = await client.PullModelAsync(modelName);

        Console.WriteLine(response.AsJson());

    }


    public static async Task PullModelStreaming()
    {
        string modelName = "all-minilm";

        OllamaClient client = GetTestClient();

        await client.DeleteModelAsync(modelName);

        StreamingResponse<PullModelResponse> response = await client.PullModelStreamingAsync(modelName);

        await foreach (var item in response)
        {
            Console.WriteLine(item.Status);

            Console.WriteLine($"{item.Completed / item.Total:P2}");
        }
    }


    public static async Task PushModel()
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

        OllamaClient client = GetTestClient();

        PushModelResponse response = await client.PushModelAsync(name);

        Console.WriteLine(response.AsJson());
    }


    public static async Task PushModelStreaming()
    {
        string name = "shuaihuadu/phi3:unittest";

        OllamaClient client = GetTestClient();

        await client.PushModelStreamingAsync(name);

        StreamingResponse<PushModelResponse> response = await client.PushModelStreamingAsync(name);


        await foreach (var item in response)
        {
            Console.WriteLine(item.Status);
        }
    }
}
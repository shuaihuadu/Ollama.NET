namespace Ollama.Core.ConsoleApp;

internal class Program
{
    const string Endpoint = "http://localhost:11434";

    static async Task Main(string[] args)
    {
        string model = await GetModelAsync(true);

        CodeAssistant codeAssistant = new(model, Endpoint);

        codeAssistant.Greeting();

        while (true)
        {
            string? input = Console.ReadLine();

            if (input is null || input == "/exit")
            {
                break;
            }
            else
            {
                await codeAssistant.ChatAsync(input);
            }
        }
    }

    static async Task<string> LoadModelAsync(string model)
    {
        using OllamaClient client = new(Endpoint);

        LoadModelResponse loadModelResponse = await client.LoadModelUseChatCompletionEndpointAsync(model, -1);

        if (loadModelResponse != null && loadModelResponse.Done)
        {
            return loadModelResponse.Model!;
        }
        else
        {
            return model;
        }
    }

    static async Task<string> GetModelAsync(bool load = false)
    {
        IList<ListModelItem> items = await GetModelsAsync();

        string model = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please select a model")
            .PageSize(100)
        .MoreChoicesText("[grey](Move up and down to reveal more models)[/]")
        .AddChoices(items.Select(item => item.Name)));

        if (load)
        {
            return await LoadModelAsync(model);
        }
        return model;
    }

    static async Task<IList<ListModelItem>> GetModelsAsync()
    {
        using OllamaClient client = new(Endpoint);

        ListModelResponse response = await client.ListModelsAsync();

        return response.Models;
    }
}

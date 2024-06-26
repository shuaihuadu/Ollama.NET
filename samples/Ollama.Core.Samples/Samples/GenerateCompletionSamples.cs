namespace Ollama.Core.Samples;

public class GenerateCompletionSamples : OllamaClientSampleBase
{

    public static async Task GenerateCompletion()
    {
        OllamaClient client = GetTestClient();

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(llama3, "Hello!");

        Console.WriteLine(response.AsJson());
    }

    public static async Task GenerateCompletionStreaming()
    {
        OllamaClient client = GetTestClient();

        StreamingResponse<GenerateCompletionResponse> response = await client.GenerateCompletionStreamingAsync(llama3, "Hello!");

        await foreach (GenerateCompletionResponse item in response)
        {
            Console.WriteLine(item.AsJson());
        }
    }


    public static async Task GenerateCompletion_FormatJson()
    {
        OllamaClient client = GetTestClient();

        GenerateCompletionOptions options = new()
        {
            Model = llama3,
            Prompt = "What color is the sky at different times of the day? Respond using JSON.",
            Format = "json"
        };

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(options);

        Console.WriteLine(response.Response);
    }


    public static async Task GenerateCompletion_RawMode()
    {
        //In some cases, you may wish to bypass the templating system and provide a full prompt. In this case, you can use the raw parameter to disable templating. Also note that raw mode will not return a context.
        OllamaClient client = GetTestClient();

        GenerateCompletionOptions options = new()
        {
            Model = mistral,
            Prompt = "[INST] Why is the sky blue?[/INST]",
            Raw = true
        };

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(options);


    }


    public static async Task GenerateCompletion_Reproducible_Outputs()
    {
        //For reproducible outputs, set temperature to 0 and seed to a number

        OllamaClient client = GetTestClient();

        GenerateCompletionOptions options = new()
        {
            Model = llama3,
            Prompt = "Why is the sky blud?",
            Options = new ParameterOptions
            {
                Seed = 123,
                Temperature = 0
            }
        };

        GenerateCompletionResponse response1 = await client.GenerateCompletionAsync(options);
        GenerateCompletionResponse response2 = await client.GenerateCompletionAsync(options);

        Console.WriteLine(response1.Response);
        Console.WriteLine(response2.Response);
    }


    public static async Task GenerateCompletion_WithOptions()
    {
        //If you want to set custom options for the model at runtime rather than in the Modelfile, you can do so with the options parameter.
        //This example sets every available option, but you can set any of them individually and omit the ones you do not want to override.

        OllamaClient client = GetTestClient();

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
            NumCtx = 1024,
            NumBatch = 2,
            NumGpu = 0,
            MainGpu = 0,
            LowVram = false,
            F16Kv = true,
            VocabOnly = false,
            UseMmap = true,
            UseMlock = false,
            NumThread = 8
        };

        GenerateCompletionOptions options = new()
        {
            Model = llama3,
            Prompt = "Why is the sky blue?",
            Options = parameterOptions
        };

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(options);

        Console.WriteLine(response.Response);
    }


    public static async Task GenerateCompletion_WithImages()
    {
        using HttpClient httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(600),
            BaseAddress = Endpoint
        };

        OllamaClient client = new(httpClient);

        byte[] bytes = await File.ReadAllBytesAsync(Path.Combine(AppContext.BaseDirectory, "Resources", "sk.png"));

        GenerateCompletionOptions options = new()
        {
            Model = llava,
            Prompt = "What is in this image?",
            Images = [Convert.ToBase64String(bytes)]
        };

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(options);

        Console.WriteLine(response.AsJson());
    }



    public static async Task GenerateCompletion_WithContext()
    {
        OllamaClient client = GetTestClient();

        GenerateCompletionResponse response1 = await client.GenerateCompletionAsync(llama3, "Hello! I'm Sam! 24 years old.");

        Console.WriteLine(response1.Response);

        GenerateCompletionResponse response2 = await client.GenerateCompletionAsync(new GenerateCompletionOptions
        {
            Model = llama3,
            Prompt = "What is my age?",
            Context = response1.Context
        });

        Console.WriteLine(response2.Response);
    }
}
namespace Ollama.Core.Tests;

public class GenerateCompletionTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string llama3 = "llama3";
    const string mistral = "mistral";

    [Fact]
    public async Task GenerateCompletion()
    {
        using OllamaClient client = GetTestClient();

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(llama3, "Hello!");

        Assert.NotEmpty(response.Response);

        Asserts(response);
    }

    [Fact]
    public async Task GenerateCompletionStreaming()
    {
        using OllamaClient client = GetTestClient();

        StreamingResponse<GenerateCompletionResponse> response = await client.GenerateCompletionStreamingAsync(llama3, "Hello!");

        Assert.NotNull(response);

        await foreach (GenerateCompletionResponse item in response)
        {
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
    public async Task GenerateCompletion_FormatJson()
    {
        using OllamaClient client = GetTestClient();

        GenerateCompletionOptions options = new()
        {
            Model = llama3,
            Prompt = "What color is the sky at different times of the day? Respond using JSON.",
            Format = "json"
        };

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(options);

        Assert.True(response.Response.IsValidJson());
    }

    [Fact]
    public async Task GenerateCompletion_RawMode()
    {
        //In some cases, you may wish to bypass the templating system and provide a full prompt. In this case, you can use the raw parameter to disable templating. Also note that raw mode will not return a context.
        using OllamaClient client = GetTestClient();

        GenerateCompletionOptions options = new()
        {
            Model = mistral,
            Prompt = "[INST] Why is the sky blue?[/INST]",
            Raw = true
        };

        GenerateCompletionResponse response = await client.GenerateCompletionAsync(options);


    }

    [Fact]
    public async Task GenerateCompletion_Reproducible_Outputs()
    {
        //For reproducible outputs, set temperature to 0 and seed to a number

        using OllamaClient client = GetTestClient();

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

        Assert.Equal(response1.Response, response2.Response);
    }

    [Fact]
    public async Task GenerateCompletion_WithOptions()
    {
        //If you want to set custom options for the model at runtime rather than in the Modelfile, you can do so with the options parameter.
        //This example sets every available option, but you can set any of them individually and omit the ones you do not want to override.

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

    private static void Asserts(GenerateCompletionResponse response)
    {
        Assert.NotEmpty(response.Model);
        Assert.Equal(llama3, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.True(response.Done);
        Assert.Equal("stop", response.DoneReason, ignoreCase: true);
        Assert.NotNull(response.Context);
        Assert.True(response.Context.Length > 0);
        Assert.True(response.TotalDuration > 0);
        Assert.True(response.LoadDuration > 0);
        //Assert.True(response.PromptEvalCount > 0);
        //Assert.Equal(0, response.PromptEvalCount);
        Assert.True(response.PromptEvalDuration > 0);
        Assert.True(response.EvalCount > 0);
        Assert.True(response.EvalDuration > 0);
    }
}
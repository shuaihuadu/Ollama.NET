namespace Ollama.Core.Samples;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        //Generate Completion
        //await GenerateCompletionSamples.GenerateCompletion();
        //await GenerateCompletionSamples.GenerateCompletion_WithImages();

        //Generate Completion Streaming
        //await GenerateCompletionSamples.GenerateCompletionStreaming();

        //Chat Completion
        //await ChatCompletionSamples.ChatCompletion();
        await ChatCompletionSamples.ChatCompletionWithOptions();

        //Chat Completion Streaming
        //await ChatCompletionSamples.ChatCompletionStreaming();

        //Generate Embedding
        //await GenerateEmbeddingSamples.GenerateEmbedding();


        Console.ForegroundColor = ConsoleColor.Gray;

        Console.WriteLine();
    }
}

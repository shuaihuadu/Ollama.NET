namespace Ollama.Core.Samples;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        await ChatCompletionSamples.ChatCompletion();

        Console.ForegroundColor = ConsoleColor.Gray;

        Console.WriteLine("");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

namespace Ollama.Core.Tests;

public class OllamaClientBaseTest(ITestOutputHelper output) : BaseTest(output)
{
    protected const string Endpoint = "http://localhost:11434";

    protected OllamaClient GetTestClient()
    {
        return new("http://localhost:11434", LoggerFactory);
    }
}
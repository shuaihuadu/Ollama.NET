namespace Ollama.Core.Tests;

public class OllamaClientBaseTest(ITestOutputHelper output) : BaseTest(output)
{
    protected OllamaClient GetTestClient()
    {
        return new("http://localhost:11434", LoggerFactory);
    }
}
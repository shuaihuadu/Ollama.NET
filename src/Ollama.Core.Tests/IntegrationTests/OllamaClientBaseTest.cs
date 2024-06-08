namespace Ollama.Core.Tests.IntegrationTests;

public class OllamaClientBaseTest(ITestOutputHelper output) : BaseTest(output)
{
    protected const string Endpoint = "http://localhost:11434";

    protected const string llama3 = "llama3";
    protected const string mistral = "mistral";
    protected const string llava = "llava";


    protected OllamaClient GetTestClient()
    {
        return new("http://localhost:11434", LoggerFactory);
    }
}
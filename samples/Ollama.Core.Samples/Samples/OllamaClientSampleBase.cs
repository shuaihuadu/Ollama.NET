namespace Ollama.Core.Samples;

public abstract class OllamaClientSampleBase
{
    protected const string Endpoint = "http://localhost:11434";

    protected const string llama3 = "llama3";
    protected const string mistral = "mistral";
    protected const string llava = "llava";


    protected static OllamaClient GetTestClient()
    {
        return new("http://localhost:11434");
    }
}
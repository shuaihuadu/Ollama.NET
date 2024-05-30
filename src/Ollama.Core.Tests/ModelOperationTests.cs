namespace Ollama.Core.Tests;

public class ModelOperationTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    const string model = "llama3";

    [Fact]
    public async Task LoadModel()
    {
        OllamaClient client = GetTestClient();

        LoadModel response = await client.LoadModelAsync(model);

        Assert.NotEmpty(response.Model);
        Assert.Equal(model, response.Model);
        Assert.True(response.CreatedAt > new DateTimeOffset(new DateTime(2024, 1, 1)));
        Assert.Empty(response.Response);
        Assert.True(response.Done);
    }
}
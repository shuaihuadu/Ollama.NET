namespace Ollama.Core.Tests.IntegrationTests;

public class BlobOperationTests(ITestOutputHelper output) : OllamaClientBaseTest(output)
{
    [Theory]
    [InlineData("sha256:fa304d6750612c207b8705aca35391761f29492534e90b30575e4980d6ca82f6", true)] //
    [InlineData("sha256:ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff", false)]
    public async Task CheckBlobExists(string digest, bool exists)
    {
        using OllamaClient client = GetTestClient();

        bool result = await client.CheckBlobExistsAsync(digest);

        Console.WriteLine(result);

        Assert.Equal(exists, result);
    }


    [Fact]
    public async Task CreateBlob()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Resources", "ollama-openapi.yaml");

        string digest = "sha256:fa304d6750612c207b8705aca35391761f29492534e90b30575e4980d6ca82f6";

        using OllamaClient client = GetTestClient();

        byte[] content = await File.ReadAllBytesAsync(filePath);

        await client.CreateBlobAsync(digest, content);
    }
}
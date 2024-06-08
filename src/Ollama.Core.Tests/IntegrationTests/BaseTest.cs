using Ollama.Core.Tests.Logging;

namespace Ollama.Core.Tests.IntegrationTests;

public abstract class BaseTest
{
    protected ITestOutputHelper Output { get; }
    protected ILoggerFactory LoggerFactory { get; }

    protected List<string> SimulatedInputText = [];

    protected int SimulatedInputTextIndex = 0;

    public BaseTest Console => this;

    protected BaseTest(ITestOutputHelper output)
    {
        Output = output;

        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.AddProvider(new XunitLoggerProvider(output));
        });
    }

    public void WriteLine(object? target = null)
    {
        Output.WriteLine(target?.ToString() ?? string.Empty);
    }

    public void WriteLine(string? format, params object?[] args) => Output.WriteLine(format);// ?? string.Empty, args);

    public void Write(object? target = null)
    {
        Output.WriteLine(target?.ToString() ?? string.Empty);
    }
}
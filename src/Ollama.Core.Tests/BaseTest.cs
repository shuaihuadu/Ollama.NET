using Ollama.Core.Tests.Logging;

namespace Ollama.Core.Tests;

public abstract class BaseTest
{
    protected ITestOutputHelper Output { get; }
    protected ILoggerFactory LoggerFactory { get; }

    protected List<string> SimulatedInputText = [];

    protected int SimulatedInputTextIndex = 0;

    public BaseTest Console => this;

    protected BaseTest(ITestOutputHelper output)
    {
        this.Output = output;

        this.LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.AddProvider(new XunitLoggerProvider(output));
        });
    }

    public void WriteLine(object? target = null)
    {
        this.Output.WriteLine(target?.ToString() ?? string.Empty);
    }

    public void WriteLine(string? format, params object?[] args) => this.Output.WriteLine(format);// ?? string.Empty, args);

    public void Write(object? target = null)
    {
        this.Output.WriteLine(target?.ToString() ?? string.Empty);
    }
}
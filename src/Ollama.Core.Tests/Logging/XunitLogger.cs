namespace Ollama.Core.Tests.Logging;

internal class XunitLogger(ITestOutputHelper testOutputHelper, string categoryName) : ILogger
{
    //https://stackoverflow.com/questions/46169169/net-core-2-0-configurelogging-xunit-test

    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;
    private readonly string _categoryName = categoryName;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => NoopDisposable.Instance;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        //_testOutputHelper.WriteLine(state?.ToString());
        //_testOutputHelper.WriteLine($"{_categoryName}[{eventId}]{formatter(state, exception)}");

        if (exception is not null)
        {
            _testOutputHelper.WriteLine($"{_categoryName}:{formatter(state, exception)}");
            _testOutputHelper.WriteLine(exception.ToString());
        }
        else
        {
            _testOutputHelper.WriteLine($"{_categoryName}:{state?.ToString()}");
        }
    }

    private class NoopDisposable : IDisposable
    {
        public static readonly NoopDisposable Instance = new();
        public void Dispose() { }
    }
}

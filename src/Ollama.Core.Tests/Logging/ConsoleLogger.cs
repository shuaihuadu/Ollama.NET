namespace Ollama.Core.Tests.Logging;

internal static class ConsoleLogger
{
    internal static ILogger Logger => LoggerFactory.CreateLogger<object>();

    internal static ILoggerFactory LoggerFactory => loggerFactory.Value;

    private static readonly Lazy<ILoggerFactory> loggerFactory = new(LogBuilder);

    private static ILoggerFactory LogBuilder()
    {
        return Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Trace);

            builder.AddConsole();
        });
    }
}

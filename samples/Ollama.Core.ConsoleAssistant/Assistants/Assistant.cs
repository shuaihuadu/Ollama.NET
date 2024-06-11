using System.Text;

namespace Ollama.Core.ConsoleApp;

internal abstract class Assistant(string model, string endpoint, string systemPrompt)
{
    protected virtual string AssistantMessageWithColorPattern(string message) => $"[green]{message.EscapeMarkup()}[/]";

    private readonly ChatMessageHistory _messages = new(systemPrompt);
    private readonly string _model = model;
    private readonly string _endpoint = endpoint;

    protected virtual void Write(ChatMessage? message)
    {
        this.InnerWrite(message?.ToString());
    }

    protected virtual void WriteLine(ChatMessage? message)
    {
        AnsiConsole.MarkupLine(AssistantMessageWithColorPattern(message == null ? string.Empty : message.ToString()));
    }

    protected virtual void Greeting(string message)
    {
        AnsiConsole.MarkupLine(AssistantMessageWithColorPattern($"{nameof(Assistant)}:{message}"));
    }

    public virtual async Task ChatAsync(string message)
    {
        this._messages.AddUserMessage(message);

        OllamaClient ollamaClient = new(this._endpoint);

        StringBuilder assistantMessageBuilder = new();

        StreamingResponse<ChatCompletionResponse> response = await ollamaClient.ChatCompletionStreamingAsync(this._model, this._messages);

        this.InnerWrite($"{nameof(Assistant)}:");

        await foreach (var item in response)
        {
            this.Write(item.Message);

            assistantMessageBuilder.Append(item.Message);

            Thread.Sleep(100);

            if (item.Done)
            {
                Console.WriteLine();

                this._messages.AddAssistantMessage(assistantMessageBuilder.ToString());

                assistantMessageBuilder.Clear();
            }
        }
    }


    private void InnerWrite(string? message)
    {
        string output = AssistantMessageWithColorPattern(message ?? string.Empty);

        AnsiConsole.Markup(output);
    }
}

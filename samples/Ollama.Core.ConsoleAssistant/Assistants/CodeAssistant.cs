namespace Ollama.Core.ConsoleApp;

internal class CodeAssistant(string model, string endpoint) : Assistant(model, endpoint, SystemPromptCN)
{
    const string GreetingEN = "Hello, I'm a professional programmer, if you have any question about programing, feel free to ask me!!!";

    const string SystemPrompt = "You are a professional programmer, only dealing with programming-related questions. If you encounter questions outside the programming field, please reply: Your question is beyond my knowledge, I am temporarily unable to answer.";

    const string GreetingCN = "您好！我是一个专业的程序员，只处理编程领域相关的问题。如果您有任何编程问题或需求，请随时提出。我会尽力帮助您解决问题！";

    const string SystemPromptCN = "你是一个专业的程序员，只处理编程领域相关的问题。如果遇到非编程领域的问题，请回答：您的提问超出了我的知识范围，我暂时无法回答。";

    public void Greeting()
    {
        this.Greeting(GreetingCN);
    }
}

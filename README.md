# Ollama.NET

[![NuGet](https://img.shields.io/nuget/v/Ollama.NET)](https://www.nuget.org/packages/Ollama.NET)

**The Ollama.NET is a powerful and easy-to-use library designed to simplify the integration of Ollama's services into .NET applications.**


# Getting Start 🚀

## Installation

To install **Ollama.NET**, use the NuGet package manager:

```powershell
Install-Package Ollama.NET
```

Or, if you prefer using the .NET CLI:

```powershell
dotnet add package Ollama.NET
```

For more information, visit the [NuGet package page](https://www.nuget.org/packages/Ollama.NET).


## Usage ✨

### Generate Completion
```csharp
//Set up the Ollama client
using OllamaClient client = new("http://localhost:11434");

//Generate completion
GenerateCompletionResponse response = await client.GenerateCompletionAsync("llama3", "Hello!");

//Generate completion streaming
StreamingResponse<GenerateCompletionResponse> streamingResponse = await client.GenerateCompletionStreamingAsync("llama3", "Hello!");


//Generate completion with context
GenerateCompletionResponse response1 = await client.GenerateCompletionAsync("llama3", "Hello! I'm Sam! 24 years old.");

GenerateCompletionResponse response2 = await client.GenerateCompletionAsync(new GenerateCompletionOptions
{
    Model = "llama3",
    Prompt = "What is my age?",
    Context = response1.Context
});

//llama3 generate completion response: You told me earlier: you're 24 years old!
```
### Chat Completion
```csharp

//Set up the Ollama client
using OllamaClient client = new("http://localhost:11434");

//Chat completion
ChatMessageHistory messages = [];

messages.AddUserMessage("Hello!");

ChatCompletionResponse response = await client.ChatCompletionAsync("llama3", messages);

//Chat completion streaming

ChatMessageHistory messages = [];

messages.AddUserMessage("Hello!");

StreamingResponse<ChatCompletionResponse> response = await client.ChatCompletionStreamingAsync("llama3", messages);

// Chat completion with chat history
ChatMessageHistory messages = [];

messages.AddUserMessage("Hello! What's the weather today?");
messages.AddAssistantMessage("It's rainy!");
messages.AddUserMessage("What should I do when I go out?");

ChatCompletionResponse response = await client.ChatCompletionAsync("llama3", messages);
```
### Model Operation

```csharp

//Set up the Ollama client
using OllamaClient client = new("http://localhost:11434");

//List models
ListModelResponse models = await client.ListModelsAsync();

//list running models
ListRunningModelResponse models = await client.ListRunningModelsAsync();

//Load model
LoadModelResponse response = await client.LoadModelUseGenerateCompletionEndpointAsync("llama3");
LoadModelResponse response = await client.LoadModelUseChatCompletionEndpointAsync("llama3");

//Unload model
LoadModelResponse response = await client.UnloadModelUseGenerateCompletionEndpointAsync("llama3");
LoadModelResponse response = await client.UnloadModelUseChatCompletionEndpointAsync("llama3");

//Create model
CreateModelResponse response = await client.CreateModelAsync("llama3-marios1", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");
//Create model streaming
StreamingResponse<CreateModelResponse> response = await client.CreateModelStreamingAsync("llama3-mario2", "FROM llama3\nSYSTEM You are mario from Super Mario Bros.");

//Show model
ShowModelResponse response = await client.ShowModelAsync("llama3");

//Copy model
await client.CopyModelAsync("llama3", "llama3-cp1");

//Delete model
await client.DeleteModelAsync("llama3-cp1");

//Pull model
PullModelResponse response = await client.PullModelAsync("qwen");
//Pull model streaming
StreamingResponse<PullModelResponse> response = await client.PullModelStreamingAsync("qwen");

//Push model

//https://github.com/ollama/ollama/blob/main/docs/import.md Importing (GGUF)

// 1.Sign - up for ollama.ai
//  https://www.ollama.ai/signup
// 2.Create a new model
//  https://www.ollama.ai/new
// 3.Create the model locally with your username as the namespace
//  ollama create <ollama-username>/<model-name> -f /path/to/Modelfile
// 4.Sign in the ollama account -> Ollama keys -> Add Ollama Public Key
// 5. open id_******.pub on your local, copy and paste to Ollama Public key
// 6. Push the model

PushModelResponse response = await client.PushModelAsync("username/modelname:tag");
//Push model streaming
StreamingResponse<PushModelResponse> response = await client.PushModelStreamingAsync("username/modelname:tag");
```
### Embedding
```csharp

//Set up the Ollama client
using OllamaClient client = new("http://localhost:11434");

EmbeddingResponse response = await client.GenerateEmbeddingAsync("all-minilm", "Hello Embedding!");

```


## Feedback 📧
If you encounter any issues or have suggestions for improvements, please feel free to open an issue on the project's repository.

namespace Ollama.Core;

public sealed partial class OllamaClient
{
    /// <summary>
    /// Create a model from a Modelfile with streaming response.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="modelFileContent">Contents of the Modelfile.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<StreamingResponse<CreateModel>> CreateModelStreamingAsync(string name, string modelFileContent, CancellationToken cancellationToken = default)
    {
        return await this.CreateModelStreamingAsync(new CreateModelStreamingRequest
        {
            Name = name,
            ModelFileContent = modelFileContent
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Create a model from a Modelfile.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="modelFileContent">Contents of the Modelfile.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<CreateModel> CreateModelAsync(string name, string modelFileContent, CancellationToken cancellationToken = default)
    {
        return await this.CreateModelAsync(new CreateModelRequest
        {
            Name = name,
            ModelFileContent = modelFileContent
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Create a model from a Modelfile with streaming response.<br />
    /// It is recommended to set modelfile to the content of the Modelfile rather than just set path. 
    /// This is a requirement for remote create. 
    /// Remote model creation must also create any file blobs, fields such as FROM and ADAPTER, explicitly with the server using Create a Blob and the value to the path indicated in the response.<br />
    /// <list type="bullet">
    /// <item><see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model"/><br /></item>
    /// <item><see cref="https://github.com/ollama/ollama/blob/main/docs/modelfile.md"/></item>
    /// </list>
    /// </summary>
    /// <param name="request">The create model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<StreamingResponse<CreateModel>> CreateModelStreamingAsync(CreateModelStreamingRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrEmpty(request.Name, nameof(request.Name));

        this._logger.LogDebug("Create model streaming: {name}", request.Name);

        try
        {
            HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string ResponseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Create model response content: {responseContent}", response.ResponseContent);

            return StreamingResponse<CreateModel>.CreateFromResponse(response.HttpResponseMessage, (responseMessage) => ServerSendEventAsyncEnumerator<CreateModel>.EnumerateFromSseStream(responseMessage, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for create model streaming faild. {Message}", ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Create a model from a Modelfile.<br />
    /// It is recommended to set modelfile to the content of the Modelfile rather than just set path. 
    /// This is a requirement for remote create. 
    /// Remote model creation must also create any file blobs, fields such as FROM and ADAPTER, explicitly with the server using Create a Blob and the value to the path indicated in the response.<br />
    /// <list type="bullet">
    /// <item><see cref="https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model"/><br /></item>
    /// <item><see cref="https://github.com/ollama/ollama/blob/main/docs/modelfile.md"/></item>
    /// </list>
    /// </summary>
    /// <param name="request">The create model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<CreateModel> CreateModelAsync(CreateModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrEmpty(request.Name, nameof(request.Name));

        this._logger.LogDebug("Create model: {name}", request.Name);

        try
        {
            HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Create model response content: {responseContent}", responseContent);

            CreateModel? createModel = responseContent.FromJson<CreateModel>();

            return createModel is null || string.IsNullOrWhiteSpace(createModel.Status)
                ? throw new DeserializationException(responseContent, message: $"The create model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(CreateModel)}.", innerException: null)
                : createModel;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for create model faild. {Message}", ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Unload the model and free up memory use generate completion endpoint.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model loaded response.</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    public async Task<LoadModel> UnloadModelUseGenerateCompletionEndpointAsync(string model, CancellationToken cancellationToken = default)
    {
        return await this.LoadModelAsync(new LoadModelUseGenerateCompletionRequest
        {
            Model = model,
            KeepAlive = 0

        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Unload the speficied model into memory use chat completion endpoint.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model loaded response.</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    public async Task<LoadModel> UnloadModelUseChatCompletionEndpointAsync(string model, CancellationToken cancellationToken = default)
    {
        return await this.LoadModelAsync(new LoadModelUseGenerateCompletionRequest
        {
            Model = model,
            KeepAlive = 0

        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Load the speficied model into memory use generate completion endpoint. <br />
    /// If you are using the API you can preload a model by sending the Ollama server an empty request. <br />
    /// This works with both the /api/generate and /api/chat API endpoints.<br />
    /// <see cref="https://github.com/ollama/ollama/blob/main/docs/faq.md"/>
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="keepAlive">
    /// To control how long the model is left in memory. Default is 300 seconds. <br />
    /// To preload a model and leave it in memory use: keepAlive = -1 <br />
    /// To unload the model and free up memory use: keepAlive = 0
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model loaded response.</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    public async Task<LoadModel> LoadModelUseGenerateCompletionEndpointAsync(string model, double keepAlive = 300, CancellationToken cancellationToken = default)
    {
        return await this.LoadModelAsync(new LoadModelUseGenerateCompletionRequest
        {
            Model = model,
            KeepAlive = keepAlive

        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Load the speficied model into memory use chat completion endpoint. <br />
    /// If you are using the API you can preload a model by sending the Ollama server an empty request. <br />
    /// This works with both the /api/generate and /api/chat API endpoints.<br />
    /// <see cref="https://github.com/ollama/ollama/blob/main/docs/faq.md"/>
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="keepAlive">
    /// To control how long the model is left in memory. Default is 300 seconds. <br />
    /// To preload a model and leave it in memory use: keepAlive = -1 <br />
    /// To unload the model and free up memory use: keepAlive = 0
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model loaded response.</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    public async Task<LoadModel> LoadModelUseChatCompletionEndpointAsync(string model, double keepAlive = 300, CancellationToken cancellationToken = default)
    {
        return await this.LoadModelAsync(new LoadModelUseGenerateCompletionRequest
        {
            Model = model,
            KeepAlive = keepAlive
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Load the speficied model into memory. <br />
    /// If you are using the API you can preload a model by sending the Ollama server an empty request. <br />
    /// This works with both the /api/generate and /api/chat API endpoints.<br />
    /// <see cref="https://github.com/ollama/ollama/blob/main/docs/faq.md"/>
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="loadModelEndpointType">The load model endpoint type.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model loaded response.</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    private async Task<LoadModel> LoadModelAsync(LoadModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request.Model, nameof(request.Model));

        this._logger.LogDebug("Load model: {model}", request.Model);

        try
        {
            HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Load model response content: {responseContent}", responseContent);

            LoadModel? loadModel = responseContent.FromJson<LoadModel>();

            return loadModel is null || string.IsNullOrWhiteSpace(loadModel.Model)
                ? throw new DeserializationException(responseContent, message: $"The load model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(LoadModel)}.", innerException: null)
                : loadModel;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for load model faild. {Message}", ex.Message);

            throw;
        }
    }
}
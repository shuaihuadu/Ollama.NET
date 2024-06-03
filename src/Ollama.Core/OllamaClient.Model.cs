namespace Ollama.Core;

public sealed partial class OllamaClient
{
    /// <summary>
    /// Create a model from a Modelfile with streaming response.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="modelFileContent">Contents of the Modelfile.</param>
    /// <param name="path">Path to the Modelfile</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<StreamingResponse<CreateModelResponse>> CreateModelStreamingAsync(string name, string modelFileContent, string? path = null, CancellationToken cancellationToken = default)
    {
        return await this.CreateModelStreamingAsync(new CreateModelStreamingRequest
        {
            Name = name,
            ModelFileContent = modelFileContent,
            Path = path,
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Create a model from a Modelfile.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="modelFileContent">Contents of the Modelfile.</param>
    /// <param name="path">Path to the Modelfile</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<CreateModelResponse> CreateModelAsync(string name, string modelFileContent, string? path = null, CancellationToken cancellationToken = default)
    {
        return await this.CreateModelAsync(new CreateModelRequest
        {
            Name = name,
            ModelFileContent = modelFileContent,
            Path = path,
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Unload the model and free up memory use generate completion endpoint.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model loaded response.</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    public async Task<LoadModelResponse> UnloadModelUseGenerateCompletionEndpointAsync(string model, CancellationToken cancellationToken = default)
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
    public async Task<LoadModelResponse> UnloadModelUseChatCompletionEndpointAsync(string model, CancellationToken cancellationToken = default)
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
    public async Task<LoadModelResponse> LoadModelUseGenerateCompletionEndpointAsync(string model, double keepAlive = 300, CancellationToken cancellationToken = default)
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
    public async Task<LoadModelResponse> LoadModelUseChatCompletionEndpointAsync(string model, double keepAlive = 300, CancellationToken cancellationToken = default)
    {
        return await this.LoadModelAsync(new LoadModelUseGenerateCompletionRequest
        {
            Model = model,
            KeepAlive = keepAlive
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Show information about a model including details, modelfile, template, parameters, license, and system prompt.
    /// </summary>
    /// <param name="name">The model name.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model information.</returns>
    public async Task<ShowModelResponse> ShowModelAsync(string name, CancellationToken cancellationToken = default)
    {
        return await this.ShowModelAsync(new ShowModelRequest
        {
            Name = name
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Copy a model. Creates a model with another name from an existing model.
    /// </summary>
    /// <param name="source">The source model name.</param>
    /// <param name="destination">The model name of destination.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task CopyModelAsync(string source, string destination, CancellationToken cancellationToken = default)
    {
        await this.CopyModelAsync(new CopyModelRequest
        {
            Source = source,
            Destination = destination,
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a model and its data.
    /// </summary>
    /// <param name="name">The model name to delete.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteModelAsync(string name, CancellationToken cancellationToken = default)
    {
        await this.DeleteModelAsync(new DeleteModelRequest
        {
            Name = name
        }, cancellationToken);
    }

    /// <summary>
    /// Download a model from the ollama library. <br />
    /// Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.<br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model"/>
    /// </summary>
    /// <param name="name">Name of the model to pull.</param>
    /// <param name="insecure">
    /// Allow insecure connections to the library. 
    /// Only use this if you are pulling from your own library during development.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <remarks>
    /// Then there is a series of downloading responses. Until any of the download is completed, the completed key may not be included. The number of files to be downloaded depends on the number of layers specified in the manifest.
    /// </remarks>
    /// <returns></returns>
    public async Task<PullModelResponse> PullModelAsync(string name, bool? insecure = null, CancellationToken cancellationToken = default)
    {
        return await this.PullModelAsync(new PullModelRequest
        {
            Name = name,
            Insecure = insecure,
        }, cancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Download a model from the ollama library with streaming. <br />
    /// Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.<br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model"/>
    /// </summary>
    /// <param name="name">Name of the model to pull.</param>
    /// <param name="insecure">
    /// Allow insecure connections to the library. 
    /// Only use this if you are pulling from your own library during development.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <remarks>
    /// Then there is a series of downloading responses. Until any of the download is completed, the completed key may not be included. The number of files to be downloaded depends on the number of layers specified in the manifest.
    /// </remarks>
    /// <returns></returns>
    public async Task<StreamingResponse<PullModelResponse>> PullModelStreamingAsync(string name, bool? insecure = null, CancellationToken cancellationToken = default)
    {
        return await this.PullModelStreamingAsync(new PullModelStreamingRequest
        {
            Name = name,
            Insecure = insecure,
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Upload a model to a model library. Requires registering for ollama.ai and adding a public key first. <br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-model"/>
    /// </summary>
    /// <param name="name">Name of the model to push in the form of &lt;namespace&gt;/&lt;model&gt;:&lt;tag&gt;. </param>
    /// <param name="insecure">
    /// Allow insecure connections to the library. 
    /// Only use this if you are pulling from your own library during development.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<PushModelResponse> PushModelAsync(string name, bool? insecure = null, CancellationToken cancellationToken = default)
    {
        return await this.PushModelAsync(new PushModelRequest
        {
            Name = name,
            Insecure = insecure
        }, cancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Upload a model to a model library with streaming. Requires registering for ollama.ai and adding a public key first. <br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-model"/>
    /// </summary>
    /// <param name="name">Name of the model to push in the form of &lt;namespace&gt;/&lt;model&gt;:&lt;tag&gt;. </param>
    /// <param name="insecure">
    /// Allow insecure connections to the library. 
    /// Only use this if you are pulling from your own library during development.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    public async Task<StreamingResponse<PushModelResponse>> PushModelStreamingAsync(string name, bool? insecure = null, CancellationToken cancellationToken = default)
    {
        return await this.PushModelStreamingAsync(new PushModelRequest
        {
            Name = name,
            Insecure = insecure
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// List models that are available locally.
    /// </summary>
    /// <returns>The local models.</returns>
    public async Task<ListModelResponse> ListModelsAsync(CancellationToken cancellationToken = default)
    {
        this._logger.LogDebug("Listing models");

        try
        {
            using HttpRequestMessage requestMessage = new ListModelRequest().ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("List model response content: {ResponseContent}", responseContent);

            ListModelResponse? models = responseContent.FromJson<ListModelResponse>();

            return models is null
                ? throw new DeserializationException(responseContent, message: $"The list model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(ListModelResponse)}.", innerException: null)
                : models;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for list model faild. Response content: {ResponseContent}, Message: {Message}", ex.ResponseContent, ex.Message);

            throw;
        }
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
    private async Task<StreamingResponse<CreateModelResponse>> CreateModelStreamingAsync(CreateModelStreamingRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Create model streaming: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string ResponseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Create model streaming response content: {ResponseContent}", response.ResponseContent);

            return StreamingResponse<CreateModelResponse>.CreateFromResponse(response.HttpResponseMessage, (responseMessage) => ServerSendEventAsyncEnumerator<CreateModelResponse>.EnumerateFromSseStream(responseMessage, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for create model streaming faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

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
    private async Task<CreateModelResponse> CreateModelAsync(CreateModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Create model: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Create model response content: {ResponseContent}", responseContent);

            CreateModelResponse? createModel = responseContent.FromJson<CreateModelResponse>();

            return createModel is null || string.IsNullOrWhiteSpace(createModel.Status)
                ? throw new DeserializationException(responseContent, message: $"The create model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(CreateModelResponse)}.", innerException: null)
                : createModel;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for create model faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Load the speficied model into memory. <br />
    /// If you are using the API you can preload a model by sending the Ollama server an empty request. <br />
    /// This works with both the /api/generate and /api/chat API endpoints.<br />
    /// <see cref="https://github.com/ollama/ollama/blob/main/docs/faq.md"/>
    /// </summary>
    /// <param name="request">The load model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model loaded response.</returns>
    /// <exception cref="DeserializationException">When deserialize the response is null.</exception>
    private async Task<LoadModelResponse> LoadModelAsync(LoadModelRequestBase request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Model, nameof(request.Model));

        this._logger.LogDebug("Load model: {Model}", request.Model);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Load model response content: {ResponseContent}", responseContent);

            LoadModelResponse? loadModel = responseContent.FromJson<LoadModelResponse>();

            return loadModel is null || string.IsNullOrWhiteSpace(loadModel.Model)
                ? throw new DeserializationException(responseContent, message: $"The load model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(LoadModelResponse)}.", innerException: null)
                : loadModel;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for load model faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Show information about a model including details, modelfile, template, parameters, license, and system prompt.
    /// </summary>
    /// <param name="request">The show model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns>The model information.</returns>
    private async Task<ShowModelResponse> ShowModelAsync(ShowModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Show model: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Show model response content: {ResponseContent}", responseContent);

            ShowModelResponse? response = responseContent.FromJson<ShowModelResponse>();

            return response is null
                ? throw new DeserializationException(responseContent, message: $"The show model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(ShowModelResponse)}.", innerException: null)
                : response;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for show model faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Copy a model. Creates a model with another name from an existing model.
    /// </summary>
    /// <param name="request">The copy model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    private async Task CopyModelAsync(CopyModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Source, nameof(request.Source));
        Argument.AssertNotNullOrWhiteSpace(request.Destination, nameof(request.Destination));

        this._logger.LogDebug("Copy model: {Source}", request.Source);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Copy model response content: {ResponseContent}", responseContent);

        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for copy model faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Delete a model and its data.
    /// </summary>
    /// <param name="request">The delete model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <remarks>Returns a 200 OK if successful, 404 Not Found if the model to be deleted doesn't exist.</remarks>
    /// <returns></returns>
    private async Task DeleteModelAsync(DeleteModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Delete model: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Delete model response content: {ResponseContent}", responseContent);
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for delete model faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Download a model from the ollama library. <br />
    /// Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.<br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model"/>
    /// </summary>
    /// <param name="request">The pull model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <remarks>
    /// Then there is a series of downloading responses. Until any of the download is completed, the completed key may not be included. The number of files to be downloaded depends on the number of layers specified in the manifest.
    /// </remarks>
    /// <returns></returns>
    private async Task<PullModelResponse> PullModelAsync(PullModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Pull model: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Pull model response content: {ResponseContent}", responseContent);

            PullModelResponse? response = responseContent.FromJson<PullModelResponse>();

            return response is null || string.IsNullOrWhiteSpace(response.Status)
                ? throw new DeserializationException(responseContent, message: $"The pull model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(PullModelResponse)}.", innerException: null)
                : response;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for pull model faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Download a model from the ollama library streaming. <br />
    /// Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.<br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model"/>
    /// </summary>
    /// <param name="request">The pull model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <remarks>
    /// Then there is a series of downloading responses. Until any of the download is completed, the completed key may not be included. The number of files to be downloaded depends on the number of layers specified in the manifest.
    /// </remarks>
    /// <returns></returns>
    private async Task<StreamingResponse<PullModelResponse>> PullModelStreamingAsync(PullModelStreamingRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Pull model streaming: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string responseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Pull model streaming response content: {ResponseContent}", response.responseContent);

            return StreamingResponse<PullModelResponse>.CreateFromResponse(response.HttpResponseMessage, (responseMessage) => ServerSendEventAsyncEnumerator<PullModelResponse>.EnumerateFromSseStream(responseMessage, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for pull model streaming faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Upload a model to a model library. Requires registering for ollama.ai and adding a public key first. <br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-model"/> <br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/import.md"/>
    /// </summary>
    /// <param name="request">The push model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    private async Task<PushModelResponse> PushModelAsync(PushModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Push model: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (_, string responseContent) = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Push model response content: {ResponseContent}", responseContent);

            PushModelResponse? response = responseContent.FromJson<PushModelResponse>();

            return response is null || string.IsNullOrWhiteSpace(response.Status)
                ? throw new DeserializationException(responseContent, message: $"The push model response content: '{responseContent}' cannot be deserialize to an instance of {nameof(PushModelResponse)}.", innerException: null)
                : response;
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for push model faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }


    /// <summary>
    /// Upload a model to a model library streaming. <br />
    /// Requires registering for ollama.ai and adding a public key first. <br />
    /// <seealso cref="https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-model"/>
    /// </summary>
    /// <param name="request">The push model request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the initial request or ongoing streaming operation.</param>
    /// <returns></returns>
    private async Task<StreamingResponse<PushModelResponse>> PushModelStreamingAsync(PushModelRequest request, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(request, nameof(request));
        Argument.AssertNotNullOrWhiteSpace(request.Name, nameof(request.Name));

        this._logger.LogDebug("Push model streaming: {Name}", request.Name);

        try
        {
            using HttpRequestMessage requestMessage = request.ToHttpRequestMessage();

            (HttpResponseMessage HttpResponseMessage, string responseContent) response = await this.ExecuteHttpRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            this._logger.LogTrace("Push model streaming response content: {ResponseContent}", response.responseContent);

            return StreamingResponse<PushModelResponse>.CreateFromResponse(response.HttpResponseMessage, (responseMessage) => ServerSendEventAsyncEnumerator<PushModelResponse>.EnumerateFromSseStream(responseMessage, cancellationToken));
        }
        catch (HttpOperationException ex)
        {
            this._logger.LogError(ex, "Request for push model streaming faild. Request content: {Request}, Response content: {ResponseContent}, Message: {Message}", request.AsJson(), ex.ResponseContent, ex.Message);

            throw;
        }
    }
}
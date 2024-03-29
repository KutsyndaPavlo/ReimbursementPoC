﻿using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Application.Common.Behaviours;

//public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
//{
//    private readonly ILogger _logger;

//    public LoggingBehaviour(ILogger<TRequest> logger)
//    {
//        _logger = logger;
//    }

//    public async Task Process(TRequest request, CancellationToken cancellationToken)
//    {
//        var requestName = typeof(TRequest).Name;

//        _logger.LogInformation("Request: {Name}  {@Request}", requestName, request);
//    }
//}


public class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result<object>
{
    private readonly ILogger _logger;

    public RequestLoggingPipelineBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Request: {Name}  {@Request}", requestName, request);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            _logger.LogInformation(
                "Completed request {RequestName}", requestName);
        }
        else
        {
            _logger.LogWarning("Completed request {RequestName} with error", requestName);
        }

        return result;
    }
}
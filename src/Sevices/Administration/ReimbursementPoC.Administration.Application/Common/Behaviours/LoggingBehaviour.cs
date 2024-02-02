using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

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
    where TResponse : class// Result
{
    private readonly ILogger _logger;

    public RequestLoggingPipelineBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Request: {Name}  {@Request}", requestName, request);

        TResponse result = await next();

        //if (result.IsSuccess)
        //{
        //    _logger.LogInformation(
        //        "Completed request {RequestName}", requestName);
        //}
        //else
        //{
        //    using (LogContext.PushProperty("Error", result.Error, true))
        //    {
        //        _logger.LogError(
        //            "Completed request {RequestName} with error", requestName);
        //    }
        //}

        return result;
    }
}
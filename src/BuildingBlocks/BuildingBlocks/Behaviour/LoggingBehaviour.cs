using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviour
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request: {request}- response: {response}", typeof(TRequest), typeof(TResponse));
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var response = await next();
            timer.Stop();
            var timeTaken= timer.Elapsed;

            if (timeTaken.Seconds > 3)
                logger.LogWarning("[PERFORMANCE] the request: {Request}, took {TimeTaken} seconds", typeof(TRequest).Name, timeTaken.Seconds);

            logger.LogInformation("[END] Handle request: {request}- response: {response}", typeof(TRequest), typeof(TResponse));

            return response;
        }
    }
}

﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Common.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}",
                    typeof(TRequest).Name, request);
                throw;
            }
        }
    }
}

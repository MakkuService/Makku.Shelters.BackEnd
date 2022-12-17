using Makku.Shelters.Application.Interfaces;
using MediatR;
using Serilog;

namespace Makku.Shelters.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private ICurrentUserService _currentUserService;

        public LoggingBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            Log.Information("Shelters Request: {Name} {@UserId}", requestName, userId, request);

            var response = await next();

            return response;
        }
    }
}

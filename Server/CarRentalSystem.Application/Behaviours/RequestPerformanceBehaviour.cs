namespace CarRentalSystem.Application.Behaviours
{
    using System.Diagnostics;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Features.Identity;
    using Microsoft.Extensions.Logging;

    public class RequestPerformanceBehaviour<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUser _currentUser;
        private readonly IIdentity _identity;

        public RequestPerformanceBehaviour(
            ILogger<TRequest> logger, 
            ICurrentUser currentUser, 
            IIdentity identity)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUser = currentUser;
            _identity = identity;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds <= 500)
            {
                return response;
            }

            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.UserId;
            var userName = await _identity.GetUserName(userId);

            _logger.LogWarning(
                "CarRental Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName,
                elapsedMilliseconds,
                userId,
                userName ?? "Anonymous",
                request);

            return response;
        }
    }
}
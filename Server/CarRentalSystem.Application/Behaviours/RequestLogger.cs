namespace CarRentalSystem.Application.Behaviours
{
    using MediatR.Pipeline;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Features.Identity;
    using Microsoft.Extensions.Logging;

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUser _currentUser;
        private readonly IIdentity _identity;

        public RequestLogger(
            ILogger<TRequest> logger, 
            ICurrentUser currentUser, 
            IIdentity identity)
        {
            _logger = logger;
            _currentUser = currentUser;
            _identity = identity;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.UserId;
            var userName = await _identity.GetUserName(userId);

            _logger.LogInformation(
                "CarRental Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName,
                userId,
                userName ?? "Anonymous",
                request);
        }
    }
}
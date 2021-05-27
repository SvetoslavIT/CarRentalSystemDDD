namespace CarRentalSystem.Application.Features.Identity.Commands.ChangePassword
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using MediatR;

    public class ChangePasswordCommand : IRequest<Result>
    {
        public string CurrentPassword { get; set; } = default!;

        public string NewPassword { get; set; } = default!;

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IIdentity _identity;

            public ChangePasswordCommandHandler(ICurrentUser currentUser, IIdentity identity)
            {
                _currentUser = currentUser;
                _identity = identity;
            }

            public Task<Result> Handle(
                ChangePasswordCommand request,
                CancellationToken cancellationToken)
                => _identity.ChangePassword(
                    _currentUser.UserId!, 
                    request.CurrentPassword, 
                    request.NewPassword);
        }
    }
}
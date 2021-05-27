namespace CarRentalSystem.Application.Features.Identity.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dealers;
    using CarRentalSystem.Domain.Factories.Dealers;
    using Common;
    using MediatR;

    public class CreateUserCommand : UserInputModel, IRequest<Result>
    {
        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
        {
            private readonly IIdentity _identity;
            private readonly IDealerFactory _dealerFactory;
            private readonly IDealerRepository _dealerRepository;

            public CreateUserCommandHandler(
                IIdentity identity, 
                IDealerFactory dealerFactory, 
                IDealerRepository dealerRepository)
            {
                _identity = identity;
                _dealerFactory = dealerFactory;
                _dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var result = await _identity.Register(request);

                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                var dealer = _dealerFactory
                    .WithName(request.Name)
                    .WithPhoneNumber(request.PhoneNumber)
                    .Build();

                user.BecomeDealer(dealer);

                await _dealerRepository.Save(dealer, cancellationToken);

                return result;
            }
        }
    }
}
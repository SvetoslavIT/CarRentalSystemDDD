namespace CarRentalSystem.Application.Features.Dealers.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using MediatR;

    public class EditDealerCommand : EntityCommand<int>, IRequest<Result>
    {
        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public class EditDealerCommandHandler : IRequestHandler<EditDealerCommand, Result>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IDealerRepository _dealerRepository;

            public EditDealerCommandHandler(ICurrentUser currentUser, IDealerRepository dealerRepository)
            {
                _currentUser = currentUser;
                _dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(EditDealerCommand request, CancellationToken cancellationToken)
            {
                var dealer = await _dealerRepository
                    .FindByUser(_currentUser.UserId!, cancellationToken);

                if (dealer.Id != request.Id)
                {
                    return "You can not edit this dealer.";
                }

                dealer
                    .UpdateName(request.Name)
                    .UpdatePhoneNumber(request.PhoneNumber);

                await _dealerRepository.Save(dealer, cancellationToken);

                return Result.Success;
            }
        }
    }
}
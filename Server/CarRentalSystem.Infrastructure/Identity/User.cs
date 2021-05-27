namespace CarRentalSystem.Infrastructure.Identity
{
    using CarRentalSystem.Application.Features.Identity;
    using Domain.Exceptions;
    using Domain.Models.Dealers;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IUser
    {
        internal User(string email)
            : base(email)
            => Email = email;

        public Dealer? Dealer { get; private set; }

        public void BecomeDealer(Dealer dealer)
        {
            if (Dealer != null)
            {
                throw new InvalidDealerException($"User '{UserName}' is already a dealer.");
            }

            Dealer = dealer;
        }
    }
}
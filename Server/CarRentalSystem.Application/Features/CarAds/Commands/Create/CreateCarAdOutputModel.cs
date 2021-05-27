namespace CarRentalSystem.Application.Features.CarAds.Commands.Create
{
    public class CreateCarAdOutputModel
    {
        public CreateCarAdOutputModel(int carAdId) => CarAdId = carAdId;

        public int CarAdId { get; }
    }
}
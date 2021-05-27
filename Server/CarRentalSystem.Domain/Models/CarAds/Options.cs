namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;
    using static ModelConstants;

    public class Options : ValueObject
    {
        internal Options(bool hasClimateControl, int numberOfSeats, TransmissionType transmissionType)
        {
            Validate(numberOfSeats);

            HasClimateControl = hasClimateControl;
            NumberOfSeats = numberOfSeats;
            TransmissionType = transmissionType;
        }

        private Options(bool hasClimateControl, int numberOfSeats)
        {
            HasClimateControl = hasClimateControl;
            NumberOfSeats = numberOfSeats;

            TransmissionType = default!;
        }

        public bool HasClimateControl { get; }

        public int NumberOfSeats { get; }

        public TransmissionType TransmissionType { get; }

        private static void Validate(int numberOfSeats)
            => Guard.AgainstOutOfRange<InvalidOptionsException>(
                numberOfSeats,
                MinNumberOfSeats,
                MaxNumberOfSeats,
                nameof(NumberOfSeats));
    }
}
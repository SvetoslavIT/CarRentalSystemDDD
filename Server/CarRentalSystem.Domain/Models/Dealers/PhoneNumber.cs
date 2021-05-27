namespace CarRentalSystem.Domain.Models.Dealers
{
    using System.Text.RegularExpressions;
    using Common;
    using Exceptions;
    using static ModelConstants;

    public class PhoneNumber : ValueObject
    {
        internal PhoneNumber(string number)
        {
            Validate(number);

            Number = number;
        }

        public string Number { get; }

        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Number;

        public static implicit operator PhoneNumber(string number) => new(number);

        private static void Validate(string number)
        {
            Guard.ForStringLength<InvalidPhoneNumberException>(
                number, 
                PhoneNumberMinLength, 
                PhoneNumberMaxLength,
                nameof(Number));

            if (!Regex.IsMatch(number, PhoneNumberPattern))
            {
                throw new InvalidPhoneNumberException(
                    "'PhoneNumber' must be start with '+' and following with numbers.");
            }
        }
    }
}
namespace CarRentalSystem.Domain.Exceptions
{
    public class InvalidOptionsException : BaseDomainException
    {
        public InvalidOptionsException()
        {
        }

        public InvalidOptionsException(string message) => Message = message;
    }
}
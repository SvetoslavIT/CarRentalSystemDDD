namespace CarRentalSystem.Domain.Exceptions
{
    using System;

    public abstract class BaseDomainException : Exception
    {
        private string? _message;

        public new string Message
        {
            get => _message ?? base.Message;
            set => _message = value;
        }
    }
}

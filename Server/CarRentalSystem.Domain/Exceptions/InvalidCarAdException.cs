﻿namespace CarRentalSystem.Domain.Exceptions
{
    public class InvalidCarAdException : BaseDomainException
    {
        public InvalidCarAdException()
        {
        }

        public InvalidCarAdException(string message) => Message = message;
    }
}
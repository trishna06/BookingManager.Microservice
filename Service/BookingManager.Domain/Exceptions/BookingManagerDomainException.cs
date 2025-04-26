using System;
using Microservice.Utility.Exception;

namespace BookingManager.Domain.Exceptions
{
    public class BookingManagerDomainException : ArcstoneException
    {
        public BookingManagerDomainException() : base()
        { }

        public BookingManagerDomainException(string message)
            : base(message)
        { }

        public BookingManagerDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

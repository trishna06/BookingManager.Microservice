namespace BookingManager.Domain.Exceptions
{
    public class BookingManagerArgumentException : BookingManagerDomainException
    {
        public BookingManagerArgumentException(string message) : base(message)
        {
        }
    }
}

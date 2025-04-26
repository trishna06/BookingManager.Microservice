namespace BookingManager.Domain.Exceptions
{
    public class BookingNotFoundException : BookingManagerDomainException
    {
        public BookingNotFoundException(int id) : base($"Booking with id '{id}' not found")
        { }
    }
}

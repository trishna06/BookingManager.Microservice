namespace BookingManager.Domain.Exceptions
{
    public class BookingManagerExistedException : BookingManagerDomainException
    {
        public BookingManagerExistedException(string userName, int roomId) : base($"Booking with user '{userName}' and room id '{roomId}' already exist")
        { }
    }
}

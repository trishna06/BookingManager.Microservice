using Microservice.Utility.Domain.SeedWork;

namespace BookingManager.Domain.Repositories
{
    public interface IBookingRepository : IRepository<Aggregates.BookingAggregate.Booking>
    {
    }
}

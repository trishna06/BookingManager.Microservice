using Microservice.Utility.Application.SeedWork;
using BookingManager.Domain.Aggregates.BookingAggregate;

namespace ContentManager.Application.Queries.Specifications.ContentAggregate
{
    public sealed class BookingByIdSpecification : BaseSpecification<Booking>
    {
        public BookingByIdSpecification(int id) : base(Booking => Booking.Id == id)
        {
        }
    }
}

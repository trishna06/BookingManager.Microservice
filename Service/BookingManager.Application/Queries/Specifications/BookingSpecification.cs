using Microservice.Utility.Application.SeedWork;
using BookingManager.Domain.Aggregates.BookingAggregate;

namespace ContentManager.Application.Queries.Specifications.ContentAggregate
{
    public sealed class BookingSpecification : BaseSpecification<Booking>
    {
        public BookingSpecification() : base(_ => true)
        {
        }
    }
}

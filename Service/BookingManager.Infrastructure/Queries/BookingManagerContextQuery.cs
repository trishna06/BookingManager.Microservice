using Microservice.Utility.Infrastructure;

namespace BookingManager.Infrastructure.Queries
{
    public class BookingManagerContextQuery : ContextQueryBase<BookingManagerContext>
    {
        public BookingManagerContextQuery(BookingManagerContext context) : base(context)
        {
        }
    }
}

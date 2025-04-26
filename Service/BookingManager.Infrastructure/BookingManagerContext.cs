using BookingManager.Infrastructure.EntityConfigurations;
using MediatR;
using Microservice.Utility.Domain.SeedWork;
using Microservice.Utility.Infrastructure;
using Microservice.Utility.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.Infrastructure
{
    public class BookingManagerContext : MicroserviceContextBase
    {
        // TODO: Please replace this with the initials prefix for the microservice. Eg. JM for Job Manager.
        public const string TablePrefix = "BM";
        public DbSet<Domain.Aggregates.BookingAggregate.Booking> Booking { get; set; }

        public BookingManagerContext(DbContextOptions options, IMediator mediator,
            ICustomFieldManager cfManager, IUserService userService) : base(TablePrefix, options, mediator, cfManager, userService)
        {
        }

        protected override void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookingEntityTypeConfiguration());
        }
    }
}

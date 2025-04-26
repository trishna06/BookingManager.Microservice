using Microservice.Utility.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManager.Infrastructure.EntityConfigurations
{
    public class BookingEntityTypeConfiguration : BaseEntityTypeConfiguration<Domain.Aggregates.BookingAggregate.Booking>
    {
        public override void Configure(EntityTypeBuilder<Domain.Aggregates.BookingAggregate.Booking> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.UserName).IsRequired();
        }
    }
}

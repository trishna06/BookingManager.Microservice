using System;
using System.Threading.Tasks;
using Microservice.Utility.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using BookingManager.Domain.Exceptions;
using BookingManager.Domain.Repositories;

namespace BookingManager.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingManagerContext _context;

        public BookingRepository(BookingManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Domain.Aggregates.BookingAggregate.Booking> AddAsync(Domain.Aggregates.BookingAggregate.Booking aggregate)
        {
            if (await _context.Booking.AnyAsync(j => j.UserName == aggregate.UserName && j.RoomId == aggregate.RoomId))
                throw new BookingManagerExistedException(aggregate.UserName, aggregate.RoomId);
            return _context.Booking.Add(aggregate).Entity;
        }

        public Task<Domain.Aggregates.BookingAggregate.Booking> GetAsync(int id)
        {
            return _context.Booking.FirstOrDefaultAsync(j => j.Id == id);
        }
    }
}

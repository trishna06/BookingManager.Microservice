using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BookingManager.Domain.Aggregates.BookingAggregate;
using BookingManager.Domain.Exceptions;
using BookingManager.Domain.Repositories;

namespace BookingManager.Application.Commands.BookingAggregate
{
    public class DeleteBookingCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteBookingCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteBookingCommandCommandHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _repository;

        public DeleteBookingCommandCommandHandler(IBookingRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
        {
            Booking Booking = await _repository.GetAsync(command.Id) ?? throw new BookingNotFoundException(command.Id);
            Booking.Delete();
            await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using BookingManager.Domain.Aggregates.BookingAggregate;
using BookingManager.Domain.Exceptions;
using BookingManager.Domain.Repositories;
using MediatR;

namespace BookingManager.Application.Commands.BookingAggregate
{
    public class UpdateBookingCommand : IRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
    public class UpdateRoomCommandCommandHandler : IRequestHandler<UpdateBookingCommand>
    {
        private readonly IBookingRepository _repository;

        public UpdateRoomCommandCommandHandler(IBookingRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
        {
            Booking booking = await _repository.GetAsync(command.Id) ?? throw new BookingNotFoundException(command.Id);
            booking.UpdateStatus(command.Status);
            await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}

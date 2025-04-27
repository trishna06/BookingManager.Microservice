using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BookingManager.Domain.Aggregates.BookingAggregate;
using BookingManager.Domain.Exceptions;
using BookingManager.Domain.Repositories;
using BookingManager.Application.Commands.DataTransferObjects;
using BookingManager.Application.Helpers;

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
        private readonly KafkaProducerHelper _kafkaProducer;

        public DeleteBookingCommandCommandHandler(IBookingRepository repository, KafkaProducerHelper kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }
        public async Task Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
        {
            Booking booking = await _repository.GetAsync(command.Id) ?? throw new BookingNotFoundException(command.Id);
            booking.Delete();
            await _repository.UnitOfWork.SaveEntitiesAsync();
            await _kafkaProducer.ProduceAsync(new RoomAvailabilityDto()
            {
                Status = "Available",
                RoomId = booking.RoomId,
                Type = "Guest"
            });
        }
    }
}

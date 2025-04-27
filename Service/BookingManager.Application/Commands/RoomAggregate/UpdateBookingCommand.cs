using System.Threading;
using System.Threading.Tasks;
using BookingManager.Application.Commands.DataTransferObjects;
using BookingManager.Application.Helpers;
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
        private readonly KafkaProducerHelper _kafkaProducer;

        public UpdateRoomCommandCommandHandler(IBookingRepository repository, KafkaProducerHelper kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }
        public async Task Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
        {
            Booking booking = await _repository.GetAsync(command.Id) ?? throw new BookingNotFoundException(command.Id);
            booking.UpdateStatus(command.Status);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            if (command.Status.ToLower() == "checkout" || command.Status.ToLower() == "cancel")
            {
                await _kafkaProducer.ProduceAsync(new RoomAvailabilityDto()
                {
                    Status = "Available",
                    RoomId = booking.RoomId,
                    Type = "Guest"
                });
            }
        }
    }
}

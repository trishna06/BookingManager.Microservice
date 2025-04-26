using System.Threading;
using System.Threading.Tasks;
using BookingManager.Application.Commands.DataTransferObjects;
using BookingManager.Application.Helpers;
using BookingManager.Domain.Aggregates.BookingAggregate;
using BookingManager.Domain.Repositories;
using MediatR;

namespace BookingManager.Application.Commands.BookingAggregate
{
    public class CreateBookingCommand : IRequest<int>
    {
        public BookingDto Booking { get; set; }
    }
    public class CreateBookingCommandCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IBookingRepository _repository;
        private readonly KafkaProducerHelper _kafkaProducer;
        public CreateBookingCommandCommandHandler(IBookingRepository repository, KafkaProducerHelper kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }
        public async Task<int> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            Booking Booking = new Booking(command.Booking.UserName, command.Booking.RoomId, command.Booking.StartDate, command.Booking.EndDate);
            await _repository.AddAsync(Booking);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _kafkaProducer.ProduceAsync(new RoomAvailabilityDto()
            {
                Status = "Booked",
                RoomId = command.Booking.RoomId,
                Type = "Guest"
            });
            return Booking.Id;
        }
    }
}

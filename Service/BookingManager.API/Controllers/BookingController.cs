using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookingManager.Application.Commands.BookingAggregate;
using BookingManager.Application.Helpers;
using BookingManager.Application.Queries;

namespace BookingManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBookingQueries _queries;
        private readonly KafkaProducerHelper _kafkaProducer;

        public BookingController(IMediator mediator, IBookingQueries queries, KafkaProducerHelper kafkaProducer)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _kafkaProducer = kafkaProducer ?? throw new ArgumentNullException(nameof(kafkaProducer));
        }

        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync()
        {
            return Ok(await _queries.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingByIdAsync(int id)
        {
            return Ok(await _queries.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingCommand command)
        {
            int id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, [FromBody] UpdateBookingCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            await _mediator.Send(new DeleteBookingCommand(id));
            return NoContent();
        }
    }
}

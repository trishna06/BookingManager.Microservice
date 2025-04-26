using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ContentManager.Application.Queries.Specifications.ContentAggregate;
using Microservice.Utility.Domain.SeedWork;
using BookingManager.Application.Queries.Models;
using BookingManager.Domain.Aggregates.BookingAggregate;
using BookingManager.Domain.Exceptions;

namespace BookingManager.Application.Queries
{
    public class BookingQueries : IBookingQueries
    {
        private readonly IContextQuery _contextQuery;
        private readonly IMapper _mapper;
        public BookingQueries(IContextQuery contextQuery,
                        IMapper mapper)
        {
            _contextQuery = contextQuery ?? throw new ArgumentNullException(nameof(contextQuery));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BookingModel> GetAsync(int id)
        {
            Booking content = await _contextQuery.FirstOrDefaultAsync(new BookingByIdSpecification(id)) ?? throw new BookingNotFoundException(id);
            return _mapper.Map<BookingModel>(content);
        }

        public async Task<List<BookingModel>> GetAsync()
        {
            List<Booking> content = await _contextQuery.FindAsync(new BookingSpecification());
            return _mapper.Map<List<BookingModel>>(content);
        }
    }
}

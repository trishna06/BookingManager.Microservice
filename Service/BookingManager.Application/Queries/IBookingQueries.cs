using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManager.Application.Queries.Models;

namespace BookingManager.Application.Queries
{
    public interface IBookingQueries
    {
        Task<BookingModel> GetAsync(int id);
        Task<List<BookingModel>> GetAsync();
    }
}

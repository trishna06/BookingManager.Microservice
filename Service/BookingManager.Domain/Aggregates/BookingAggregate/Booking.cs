using System;
using Microservice.Utility.Domain.SeedWork;

namespace BookingManager.Domain.Aggregates.BookingAggregate
{
    public class Booking: Entity, IAggregate
    {
        public int Id { get; protected set; }
        public string UserName { get; protected set; }
        public int RoomId { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public string Status { get; protected set; }

        public Booking()
        {

        }
        public Booking(string userName, int roomId, DateTime startDate, DateTime endDate)
        {
            UserName = userName;
            RoomId = roomId;
            StartDate = startDate;
            EndDate = endDate;
            Status = "Booked";
        }

        public void UpdateStatus(string status)
        {
            Status = status;
        }
    }

}

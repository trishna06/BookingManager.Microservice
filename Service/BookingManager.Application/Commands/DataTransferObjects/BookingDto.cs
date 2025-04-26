using System;

namespace BookingManager.Application.Commands.DataTransferObjects
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}

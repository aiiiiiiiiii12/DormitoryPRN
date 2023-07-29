using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public string Account { get; set; } = null!;
        public DateTime InDate { get; set; }
        public bool Confirmroom { get; set; }

        public virtual User AccountNavigation { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}

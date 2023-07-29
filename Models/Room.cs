using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Roomid { get; set; }
        public string Roomname { get; set; } = null!;
        public int BuildingId { get; set; }
        public int RtypeId { get; set; }
        public string? RoomImg { get; set; }
        public int? Member { get; set; }

        public virtual Building Building { get; set; } = null!;
        public virtual Roomtype Rtype { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}

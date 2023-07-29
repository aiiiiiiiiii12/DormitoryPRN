using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Roomtype
    {
        public Roomtype()
        {
            Rooms = new HashSet<Room>();
        }

        public int RtypeId { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}

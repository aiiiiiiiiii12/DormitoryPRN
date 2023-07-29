using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Building
    {
        public Building()
        {
            Rooms = new HashSet<Room>();
        }

        public int BuildingId { get; set; }
        public string Buildingname { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            Feedbacks = new HashSet<Feedback>();
        }

        public string Account { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string? Address { get; set; }
        public decimal? Money { get; set; }
        public bool? Inroom { get; set; }
        public string? Roles { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}

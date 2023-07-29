using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Feedback
    {
      
        public string? Feedback1 { get; set; }
        public string? Felling { get; set; }
        public int Id { get; set; }
        public string? Account { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public virtual User? AccountNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Admin
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AirSpring.DAL.DataContext
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Status { get; set; }
    }
}

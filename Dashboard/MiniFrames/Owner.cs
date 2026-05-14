using System;
using System.Collections.Generic;
using System.Text;

namespace MiniFrames
{
    public class Owner
    {
        public int OwnerID { get; set; }
        public string Name { get; set; } = "";
        public string ContactNumber { get; set; } = "";
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MiniFrames
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "Staff";
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AtPin { get; set; } = string.Empty;
    }
}

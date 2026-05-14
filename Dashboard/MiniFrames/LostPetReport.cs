using System;
using System.Collections.Generic;
using System.Text;

namespace MiniFrames
{
    public class LostPetReport
    {
        public int ReportID { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public DateTime DateLost { get; set; }
        public string LastSeenLocation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

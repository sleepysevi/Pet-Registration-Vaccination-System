using System;
using System.Collections.Generic;
using System.Text;

namespace MiniFrames
{
    public class Vaccination
    {
        public int VaccinationID { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public string DateGiven { get; set; } = string.Empty;
        public string NextDueDate { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}

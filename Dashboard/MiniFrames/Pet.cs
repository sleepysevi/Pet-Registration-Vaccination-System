using System;
using System.Collections.Generic;
using System.Text;

namespace MiniFrames
{
    public class Pet
    {
        public int PetID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Age { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public string LastVaccineDate { get; set; } = string.Empty;
        public string VaccineStatus { get; set; } = string.Empty;
        public string VaccineDaysLeft { get; set; } = string.Empty;
        public string LostReportDate { get; set; } = string.Empty;
        public string LostReportLocation { get; set; } = string.Empty;
    }
}

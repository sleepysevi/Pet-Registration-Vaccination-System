namespace AlagaTrack
{
    public class VaccinationRecord
    {
        public int VaccinationID { get; set; }
        public int PetID { get; set; }
        public string PetName { get; set; } = "";
        public string OwnerName { get; set; } = "";
        public string DateGiven { get; set; } = "";
        public string NextDueDate { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}

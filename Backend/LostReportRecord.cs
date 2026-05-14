namespace AlagaTrack
{
    public class LostReportRecord
    {
        public int ReportID { get; set; }
        public string OwnerName { get; set; } = "";
        public string PetName { get; set; } = "";
        public string DateLost { get; set; } = "";
        public string LastSeenLocation { get; set; } = "";
        public string Description { get; set; } = "";
        public string Status { get; set; } = "active";
    }
}

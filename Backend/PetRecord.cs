using System.Collections.Generic;

namespace AlagaTrack
{
    public class OwnerPetGroup
    {
        public int OwnerId { get; set; }
        public string DisplayLabel { get; set; } = "";
        public List<(int PetId, string PetName)> Pets { get; set; } = new();
    }

    public class PetRecord
    {
        public int PetID { get; set; }
        public int OwnerID { get; set; }
        public string PetName { get; set; } = "";
        public string Species { get; set; } = "";
        public string Breed { get; set; } = "";
        public string Color { get; set; } = "";
        public int Age { get; set; }
        public string PetsPhoto { get; set; } = "";
        public string OwnerName { get; set; } = "";
        public string OwnerContact { get; set; } = "";
    }
}

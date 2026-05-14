using System.Collections.Generic;

namespace MiniFrames
{
    public class PetBridgeOption
    {
        public int PetId;
        public string Name = "";

        public override string ToString() => Name;
    }

    public class OwnerPetBridgeGroup
    {
        public string DisplayLabel = "";
        public List<PetBridgeOption> Pets = new();
    }
}

using ModdingLab.Define;

namespace ModdingLab.Define
{
    [System.Serializable]
    public class EntityIdentifier
    {
        public string id;
        public string name;
        public string describe;
        public string[] tags;

        public EntityIdentifier(EntityData data)
        {
            id = data.id;
            name = data.name;
            describe = data.describe;
            tags = data.tags;
        }
    }
}

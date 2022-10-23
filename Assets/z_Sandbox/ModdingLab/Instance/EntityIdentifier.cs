using ModdingLab.Definition;

namespace ModdingLab.Instance
{
    [System.Serializable]
    public class EntityIdentifier
    {
        public string id;
        public string name;
        public string describe;
        public string[] tags;

        public EntityIdentifier(EntityDefine data)
        {
            id = data.id;
            name = data.name;
            describe = data.describe;
            tags = data.tags;
        }
    }
}

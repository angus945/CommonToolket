using ModdingLaboratory.Definition;

namespace ModdingLaboratory.Instance
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
            id = data.globalID;
            name = data.name;
            describe = data.describe;
            tags = data.tags.tags.ToArray();
        }
    }
}

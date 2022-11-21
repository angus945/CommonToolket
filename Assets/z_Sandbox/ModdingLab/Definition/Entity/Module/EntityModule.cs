using System.Xml.Serialization;

namespace ModdingLaboratory.Definition
{
    public abstract class EntityModule : DefinitionBase
    {
        protected override string localID { get => id; }
        public override void SetGroup(string group)
        {
            base.SetGroup(group);

            string[] includes = this.includes;
            for (int i = 0; i < includes.Length; i++)
            {
                includes[i] = base.ApplyGroupID(includes[i]);
            }
            this.include = string.Join(" ", includes);
        }

        [XmlAttribute("id")] public string id;

        [XmlAttribute]
        public string include;

        public string[] includes
        {
            get
            {
                return string.IsNullOrEmpty(include) ? new string[0] : include.Split(' ');
            }
        }
    }
}

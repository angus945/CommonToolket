using DataDriven.XML;
using System.Collections.Generic;
using System.Xml;

namespace ModdingLab.Definition
{
    public class DefinitionTable<T> where T : class, IDefinition
    {
        public Dictionary<string, T> definitionTable = new Dictionary<string, T>();
        public Dictionary<string, T>.ValueCollection Values
        {
            get => definitionTable.Values;
        }

        public void Add(T data)
        {
            definitionTable.Add(data.id, data);
        }
        public void Add(XmlNode node)
        {
            T data = XMLConverter.ConvertNode<T>(node);

            Add(data);
        }
        public T GetDefine(string id)
        {
            if (definitionTable.TryGetValue(id, out T define))
            {
                return define;
            }
            else
            {
                Logger.Log($"Undefine Data, type: {typeof(T)}, id: {id}", LogType.Warning);

                return (T)default;
            }
        }
    }

}

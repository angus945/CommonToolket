using DataDriven.XML;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ModdingLab.Definition
{
    public class DefinitionTable<T> where T : class, IDefinition
    {
        Dictionary<string, T> table = new Dictionary<string, T>();
        public Dictionary<string, T>.ValueCollection Values
        {
            get => table.Values;
        }

        public void Add(T data)
        {
            table.Add(data.id, data);
        }
        public void Add(XmlNode node)
        {
            T data = XMLConverter.ConvertNode<T>(node);

            Add(data);
        }
        public bool TryGetDefine(string id, out T define)
        {
            if (table.TryGetValue(id, out define))
            {
                return true;
            }
            else
            {
                Logger.Log($"Undefine Data, type: {typeof(T)}, id: {id}", LogType.Warning);

                return false;
            }
        }

        public IEnumerable<T> GetDefines(string[] identifiers)
        {
            for (int i = 0; i < identifiers.Length; i++)
            {
                if(table.TryGetValue(identifiers[i], out T value))
                {
                    yield return value;
                }
            }
        }
    }

}

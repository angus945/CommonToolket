using DataDriven;
using DataDriven.XML;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ModdingLaboratory.Definition
{
    public class DefinitionTable<T> where T : DefinitionBase
    {
        Dictionary<string, T> table = new Dictionary<string, T>();
        public Dictionary<string, T>.ValueCollection Values
        {
            get => table.Values;
        }

        public void Add(string group, T data)
        {
            data.SetGroup(group);

            table.Add(data.globalID, data);
        }
        public void Add(string group, XmlNode node)
        {
            try
            {
                T data = XMLConverter.ConvertNode<T>(node);

                Add(group, data);
            }
            catch (Exception)
            {
                Debugger.Print($"Parsing Error, type: {typeof(T)}", LogType.Error);
                throw;
            }

        }
        public bool TryGetDefine(string id, out T define)
        {
            if (table.TryGetValue(id, out define))
            {
                return true;
            }
            else
            {
                Debugger.Print($"Undefine Data, type: {typeof(T)}, id: {id}", LogType.Warning);

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

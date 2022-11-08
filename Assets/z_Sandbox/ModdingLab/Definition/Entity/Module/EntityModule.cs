using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace ModdingLab.Definition
{
    public abstract class EntityModule<T> : IDefinition
    {
        [field:SerializeField]
        [XmlAttribute] 
        public string id { get; set; }

        protected abstract List<T> moduleContents { get; }

        public T this[int index] 
        {
            get => moduleContents[index];
        }
        public int length { get => moduleContents.Count; }
    }
}

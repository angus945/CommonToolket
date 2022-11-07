using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab.Definition
{
    public abstract class EntityModule<T>
    {
        protected abstract List<T> moduleContents { get; }

        public T this[int index] 
        {
            get => moduleContents[index];
        }
        public int length { get => moduleContents.Count; }
    }
}

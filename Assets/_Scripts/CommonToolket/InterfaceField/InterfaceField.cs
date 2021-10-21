using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolket.Interface
{
    [System.Serializable]
    public struct InterfaceField<T> where T : class
    {
        [SerializeField] GameObject reference;

        public T element { get => reference.GetComponent<T>(); }
    }
}


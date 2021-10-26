using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tokenization.DataStorge
{
    public abstract class DataStorgeToken : ScriptableObject
    {
        void Awake()
        {
            ResetData();
        }

        protected abstract void ResetData();

    }

}

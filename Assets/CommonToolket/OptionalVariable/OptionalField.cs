using System;
using UnityEngine;

namespace Toolket.Optional
{
    [Serializable]
    public struct OptionalField<T>
    {
        [SerializeField] T value;
        [SerializeField] bool enabled;
    }


}

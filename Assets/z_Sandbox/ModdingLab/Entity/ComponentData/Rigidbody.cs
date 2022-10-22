using System;
using System.Xml.Serialization;
using UnityEngine;

namespace ModdingLab
{
    [XmlType]
    [System.Serializable]
    public class Rigidbody : ComponentData
    {
        public override Type RequireComponentType { get => typeof(Rigidbody2D); }

        public override void InitialComponent(GameEntity entity, Component component)
        {
            Rigidbody2D rigidbody = component as Rigidbody2D;
        }
    }

}

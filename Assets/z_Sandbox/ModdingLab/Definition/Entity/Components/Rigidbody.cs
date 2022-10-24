using System;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLab.Instance;

namespace ModdingLab.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class Rigidbody : ComponentDefine
    {
        protected override string defaultID => "Rigidbody";

        public override Type RequireComponentType { get => typeof(Rigidbody2D); }

        public override void InitialComponent(GameEntity entity, Component component)
        {
            Rigidbody2D rigidbody = component as Rigidbody2D;


        }
    }

}

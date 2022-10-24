using System;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLab.Instance;

namespace ModdingLab.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class Collision : ComponentDefine
    {
        protected override string defaultID => "Collision";

        public override Type RequireComponentType { get => typeof(BoxCollider2D); }

        public override void InitialComponent(GameEntity entity, Component component)
        {
            BoxCollider2D collider = component as BoxCollider2D;
        }
    }

}

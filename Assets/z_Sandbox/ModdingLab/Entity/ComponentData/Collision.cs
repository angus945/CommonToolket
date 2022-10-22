using System;
using System.Xml.Serialization;
using UnityEngine;

namespace ModdingLab
{
    [XmlType]
    [System.Serializable]
    public class Collision : ComponentData
    {
        public override Type RequireComponentType { get => typeof(BoxCollider2D); }

        public override void InitialComponent(GameEntity entity, Component component)
        {
            BoxCollider2D collider = component as BoxCollider2D;
        }
    }

}

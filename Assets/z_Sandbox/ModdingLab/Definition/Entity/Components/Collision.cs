using System;
using System.Xml.Serialization;
using UnityEngine;
using ModdingLaboratory.Instance;
using DataDriven;

namespace ModdingLaboratory.Definition.Componentized
{
    [XmlType]
    [System.Serializable]
    public class Collision : ComponentDefine
    {
        protected override string defaultID => "Collision";

        public override Type RequireComponentType 
        { 
            get
            {
                switch (type)
                {
                    case "box":
                        return typeof(BoxCollider2D);

                    case "capsule":
                        return typeof(CapsuleCollider2D);

                    case "circle":
                    default:
                        return typeof(CircleCollider2D);
                }
            } 
        }

        [XmlAttribute]
        public string type;
        //box, circle, capsule

        [XmlAttribute]
        public string offset;

        [XmlAttribute]
        public string size;

        [XmlAttribute]
        public float radius;

        public override void InitialComponent(Component component)
        {
            //Vector2 offset = this.offset

            switch (component)
            {
                case BoxCollider2D box:
                    if(!string.IsNullOrEmpty(offset))
                    {
                        box.offset = offset.ParseToVector2();
                    }
                    if(!string.IsNullOrEmpty(size))
                    {
                        box.size = size.ParseToVector2();
                    }
                    break;

                case CapsuleCollider2D capsule:
                    if(!string.IsNullOrEmpty(offset))
                    {
                        capsule.offset = offset.ParseToVector2();
                    }
                    if(!string.IsNullOrEmpty(size))
                    {
                        capsule.size = size.ParseToVector2();
                    }
                    break;

                case CircleCollider2D circle:
                    if(radius != 0) circle.radius = radius;
                    break;
                
            }
        }
    }

}

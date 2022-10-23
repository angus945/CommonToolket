using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using ModdingLab.Instance.Visual;
using ModdingLab.Instance.Behavior;

namespace ModdingLab.Instance
{

    [MoonSharpUserData]
    public class GameEntity : MonoBehaviour
    {
        public EntityIdentifier identifier { get; private set; }

        Dictionary<string, float> properties;
        Dictionary<string, SpriteSheet> spriteSheets;
        Dictionary<string, Component> components;
        Dictionary<string, LuaBehavior> behaviours;

        //
        public void Initial(EntityIdentifier identifier)
        {
            this.identifier = identifier;

            components = new Dictionary<string, Component>();
            spriteSheets = new Dictionary<string, SpriteSheet>();
            properties = new Dictionary<string, float>();
        }
        public void AddProperity(string name, float value)
        {
            properties.Add(name, value);
        }
        public void AddSpriteSheet(SpriteSheet sheet)
        {
            if (sheet == null) return;

            spriteSheets.Add(sheet.id, sheet);
        }
        public void AddComponent(string id, Component component)
        {
            components.Add(id, component);
        }
        public void AddBehavior(string id, LuaBehavior behavior)
        {
            behaviours.Add(id, behavior);
        }

        //
        public float GetProperity(string name)
        {
            if(properties.TryGetValue(name, out float value))
            {
                return value;
            }

            return 0;
        }
        public SpriteSheet GetSpriteSheetByID(string id)
        {
            if (spriteSheets.TryGetValue(id, out SpriteSheet sheet))
            {
                return sheet;
            }

            return null;
        }
        public Component GetComponentByID(string id)
        {
            if (components.TryGetValue(id, out Component component))
            {
                return component;
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using ModdingLaboratory.Instance.Visual;
using ModdingLaboratory.Instance.Behavior;

namespace ModdingLaboratory.Instance
{

    [MoonSharpUserData]
    public class GameEntity : MonoBehaviour
    {
        public EntityIdentifier identifier { get; private set; }

        Dictionary<string, float> properties;
        Dictionary<string, SpriteSheet> spriteSheets;
        Dictionary<string, Component> components;
        Dictionary<string, LuaBehavior> behaviours;
        //Dictionary<string, List<DynValue>> eventListeners;

        //
        void Start()
        {

        }
        void Update()
        {
            UpdateBehaviors(Time.deltaTime);
        }

        void UpdateBehaviors(float delta)
        {
            foreach (var behavior in behaviours)
            {
                behavior.Value.Update(delta);
            }
        }

        //
        public void Initial(EntityIdentifier identifier)
        {
            this.identifier = identifier;

            components = new Dictionary<string, Component>();
            spriteSheets = new Dictionary<string, SpriteSheet>();
            properties = new Dictionary<string, float>();
            behaviours = new Dictionary<string, LuaBehavior>();
        }
        public void AddProperity(string id, float value)
        {
            if (properties.ContainsKey(id)) return;

            properties[id] = value;
        }
        public void AddSpriteSheet(string id, SpriteSheet sheet)
        {
            if (spriteSheets.ContainsKey(id)) return;

            spriteSheets.Add(id, sheet);
        }
        public void AddComponent(string id, Component component)
        {
            if (components.ContainsKey(id)) return;

            components[id] = component;
        }
        public void AddBehavior(string id, LuaBehavior behavior)
        {
            if (properties.ContainsKey(id)) return;

            behaviours.Add(id, behavior);
        }

        //
        public bool ContainsComponent(string id)
        {
            return components.ContainsKey(id);
        }
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

            return default;
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

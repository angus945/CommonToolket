using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using ModdingLab.Instance.Visual;

namespace ModdingLab.Instance
{

    [MoonSharpUserData]
    public class GameEntity : MonoBehaviour
    {
        public EntityIdentifier identifier { get; private set; }

        Dictionary<string, Component> components;
        Dictionary<string, SpriteSheet> spriteSheets;

        //
        public void Initial(EntityIdentifier identifier)
        {
            this.identifier = identifier;

            components = new Dictionary<string, Component>();
            spriteSheets = new Dictionary<string, SpriteSheet>();
        }
        public void AddComponent(string id, Component component)
        {
            components.Add(id, component);
        }
        public void AddSpriteSheet(SpriteSheet sheet)
        {
            if (sheet == null) return;

            spriteSheets.Add(sheet.id, sheet);
        }

        //
        public Component GetComponentByID(string id)
        {
            if (components.TryGetValue(id, out Component component))
            {
                return component;
            }

            return null;
        }
        public SpriteSheet GetSpriteSheetByID(string id)
        {
            if (spriteSheets.TryGetValue(id, out SpriteSheet sheet))
            {
                return sheet;
            }

            return null;
        }
    }
}

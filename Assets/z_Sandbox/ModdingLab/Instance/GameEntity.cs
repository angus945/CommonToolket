using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using ModdingLab.Definition;
using ModdingLab.Definition.Componentized;

namespace ModdingLab.Instance
{

    [MoonSharpUserData]
    public class GameEntity : MonoBehaviour
    {
        public static GameEntity CreateEntity(EntityDefine data)
        {
            GameObject entityObject = new GameObject(data.name);

            GameEntity entity = entityObject.AddComponent<GameEntity>();
            entity.identifier = new EntityIdentifier(data);

            for (int i = 0; i < data.spriteSheets.Length; i++)
            {
                string sheetID = data.spriteSheets[i];
                entity.AddSpriteSheet(sheetID);
            }
            for (int i = 0; i < data.components.Length; i++)
            {
                ComponentData component = data.components[i];
                entity.AddComponent(component);
            }

            return entity;
        }

        EntityIdentifier identifier;

        //
        Dictionary<string, Component> components = new Dictionary<string, Component>();
        Dictionary<string, SpriteSheetDefine> spriteSheets = new Dictionary<string, SpriteSheetDefine>();

        //
        public void AddComponent(ComponentData data)
        {
            Component component = gameObject.AddComponent(data.RequireComponentType);
            data.InitialComponent(this, component);

            components.Add(data.id, component);
        }
        public void AddSpriteSheet(string sheedID)
        {
            if (DefinitionTables.spriteSheetDefineTable.TryGetValue(sheedID, out SpriteSheetDefine sheet))
            {
                spriteSheets.Add(sheedID, sheet);
            }
            else
            {
                Debug.LogWarning($"Undefine SpriteSheet: {sheedID}");
            }
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
        public SpriteSheetDefine GetSpriteSheetByID(string id)
        {
            if (spriteSheets.TryGetValue(id, out SpriteSheetDefine sheet))
            {
                return sheet;
            }

            return null;
        }
    }
}

using DataDriven.Lua;
using ModdingLab.Definition;
using ModdingLab.Definition.Componentized;
using ModdingLab.Definition.TypeScript;
using ModdingLab.Instance.Behavior;
using ModdingLab.Instance.Visual;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab.Instance
{
    public class EntityInstantiator
    {

        public static GameEntity CreateEntity(string entityID)
        {
            Logger.Tick(null);

            if (DefinitionTables.GetEntityDefine(entityID, out EntityDefine define))
            {
                GameEntity entity = CreateEntity(define);

                SetEntity(entity, define);

                Logger.Tick("Entity Instance");
                return entity;
            }
            else
            {
                return null;
            }
        }

        static GameEntity CreateEntity(EntityDefine define)
        {
            GameObject entityObject = new GameObject(define.name);

            GameEntity entity = entityObject.AddComponent<GameEntity>();

            SetIdentifier(entity, define);

            return entity;
        }
        static void SetEntity(GameEntity entity, EntityDefine define)
        {
            SetSpriteSheet(entity, define.spriteSheets.spriteSheets);
            SetProperties(entity, define.properties);
            SetComponents(entity, define.components.components);
            SetBehaviors(entity, define.behaviors.behaviors);
        }
        static void SetIdentifier(GameEntity entity, EntityDefine define)
        {
            EntityIdentifier identifier = new EntityIdentifier(define);
            entity.Initial(identifier);
        }
        static void SetSpriteSheet(GameEntity entity, List<string> spriteSheets)
        {
            if (spriteSheets == null) return;

            for (int i = 0; i < spriteSheets.Count; i++)
            {
                string sheetID = spriteSheets[i];
                SpriteSheet sheet = DefinitionTables.GetSpriteSheet(sheetID);

                entity.AddSpriteSheet(sheet);
            }
        }
        static void SetProperties(GameEntity entity, EntityProperties properties)
        {
            if (properties == null) return;

            for (int i = 0; i < properties.length; i++)
            {
                ProperityField properity = properties[i];
                entity.AddProperity(properity.name, properity.value);
            }
        }
        static void SetComponents(GameEntity entity, List<ComponentDefine> components)
        {
            if (components == null) return;

            for (int i = 0; i < components.Count; i++)
            {
                ComponentDefine define = components[i];

                Component component = entity.gameObject.AddComponent(define.RequireComponentType);

                define.InitialComponent(entity, component);
                entity.AddComponent(define.id, component);
            }
        }
        static void SetBehaviors(GameEntity entity, List<BehaviorDefine> behaviors)
        {
            if (behaviors == null) return;

            for (int i = 0; i < behaviors.Count; i++)
            {
                BehaviorDefine behaviorDefine = behaviors[i];

                string code = DefinitionTables.GetScriptCode(behaviorDefine.scriptName);
                if (string.IsNullOrEmpty(code)) continue;

                Script script = LuaInitializer.CreateScript(code);
                script.Globals["Entity"] = entity;

                LuaFunction[] functions = Array.ConvertAll(behaviorDefine.functions, n => new LuaFunction(n.functionName, n.call, (int)n.type));
                LuaBehavior behavior = new LuaBehavior(behaviorDefine.active, script, functions);

                entity.AddBehavior(behaviorDefine.id, behavior);
            }
        }



    }
}

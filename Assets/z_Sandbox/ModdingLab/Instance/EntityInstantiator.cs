using UnityEngine;
using ModdingLab.Definition;
using ModdingLab.Definition.Componentized;
using ModdingLab.Definition.TypeScript;
using ModdingLab.Instance.Visual;
using ModdingLab.Instance.Behavior;
using System;
using MoonSharp.Interpreter;
using DataDriven.Lua;

namespace ModdingLab.Instance
{
    public class EntityInstantiator
    {

        public static GameEntity CreateEntity(string entityID)
        {
            Logger.Tick(null);

            EntityDefine define = DefinitionTables.GetEntityDefine(entityID);
            GameEntity entity = CreateEntity(define);

            Logger.Tick("Entity Instance");

            return entity;
        }
        static GameEntity CreateEntity(EntityDefine define)
        {
            GameObject entityObject = new GameObject(define.name);

            GameEntity entity = entityObject.AddComponent<GameEntity>();

            entity.Initial(new EntityIdentifier(define));

            for (int i = 0; i < define.spriteSheets.Length; i++)
            {
                string sheetID = define.spriteSheets[i];
                SpriteSheet sheet = DefinitionTables.GetSpriteSheet(sheetID);

                entity.AddSpriteSheet(sheet);
            }
            for (int i = 0; i < define.properties.Length; i++)
            {
                ProperityField properity = define.properties[i];
                entity.AddProperity(properity.name, properity.value);
            }
            for (int i = 0; i < define.components.Length; i++)
            {
                ComponentDefine componentDefine = define.components[i];

                Component component = entityObject.AddComponent(componentDefine.RequireComponentType);

                componentDefine.InitialComponent(entity, component);
                entity.AddComponent(componentDefine.id, component);
            }
            for (int i = 0; i < define.behaviors.Length; i++)
            {
                BehaviorDefine behaviorDefine = define.behaviors[i];

                string code = DefinitionTables.GetScriptCode(behaviorDefine.scriptName);
                if (string.IsNullOrEmpty(code)) continue;

                Script script = LuaInitializer.CreateScript(code);
                script.Globals["Entity"] = entity;

                LuaFunction[] functions = Array.ConvertAll(behaviorDefine.functions, n => new LuaFunction(n.functionName, n.call, (int)n.type));
                LuaBehavior behavior = new LuaBehavior(behaviorDefine.active, script, functions);

                entity.AddBehavior(behaviorDefine.id, behavior);
            }

            return entity;
        }
    }
}

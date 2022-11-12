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

            GameEntity entity = null;

            if (EntityDatabase.TryGetEntity(entityID, out EntityDefine define, out EntityModule[] modules))
            {
                entity = CreateEntity(define, modules);
            }

            Logger.Tick("Entity Instance");

            return entity;
        }

        static GameEntity CreateEntity(EntityDefine define, EntityModule[] modules)
        {
            GameObject entityObject = new GameObject(define.name);

            GameEntity entity = entityObject.AddComponent<GameEntity>();
            SetIdentifier(entity, define);

            AddModule(entity, define.spriteSheets);
            AddModule(entity, define.properties);
            AddModule(entity, define.components);
            AddModule(entity, define.behaviors);

            for (int i = 0; i < modules.Length; i++)
            {
                AddModule(entity, modules[i]);
            }

            return entity;
        }
        static void SetIdentifier(GameEntity entity, EntityDefine define)
        {
            EntityIdentifier identifier = new EntityIdentifier(define);
            entity.Initial(identifier);
        }

        static void AddModule(GameEntity entity, EntityModule module)
        {
            if (module == null) return;

            switch (module)
            {
                case VisualModule visual:
                    SetSpriteSheet(entity, visual);
                    break;

                case ProperityModule properties:
                    SetProperties(entity, properties);
                    break;

                case ComponentModule components:
                    SetComponents(entity, components);
                    break;

                case BehaviorModule behaviors:
                    SetBehaviors(entity, behaviors);
                    break;
            }
        }
        static void SetSpriteSheet(GameEntity entity, VisualModule visual)
        {
            for (int i = 0; i < visual.length; i++)
            {
                string sheetID = visual[i];

                if(VisualDatabase.TryGetSpriteSheet(sheetID, out SpriteSheet sheet))
                {
                    entity.AddSpriteSheet(sheet);
                }
            }
        }
        static void SetProperties(GameEntity entity, ProperityModule properties)
        {
            for (int i = 0; i < properties.length; i++)
            {
                ProperityField properity = properties[i];
                entity.AddProperity(properity.name, properity.value);
            }
        }
        static void SetComponents(GameEntity entity, ComponentModule components)
        {
            for (int i = 0; i < components.length; i++)
            {
                ComponentDefine define = components[i];

                if(!entity.ContainsComponent(define.id))
                {
                    Component component = entity.gameObject.AddComponent(define.RequireComponentType);

                    define.InitialComponent(component);
                    entity.AddComponent(define.id, component);
                }
            }
        }
        static void SetBehaviors(GameEntity entity, BehaviorModule behaviors)
        {
            for (int i = 0; i < behaviors.length; i++)
            {
                BehaviorDefine behaviorDefine = behaviors[i];

                string code = EntityDatabase.GetBehaviorScript(behaviorDefine.scriptName);
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

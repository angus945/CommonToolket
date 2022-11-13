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

            AddModule(entity, define.spriteSheets); //GC 5.4 KB
            AddModule(entity, define.properties); //GC 5.2 KB
            AddModule(entity, define.components); //GC 5.2 KB
            AddModule(entity, define.behaviors); //GC 2.4MB

            for (int i = 0; i < modules.Length; i++) //GC 51 KB
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
                BehaviorDefine define = behaviors[i];

                if(EntityDatabase.GetBehaviorScript(define.scriptName, out Script script))
                {
                    LuaFunction[] functions = Array.ConvertAll(define.functions, n =>
                    {
                        DynValue function = script.Globals.Get(n.functionName);

                        return new LuaFunction(n.call, (int)n.type, entity, function);
                    });

                    LuaBehavior behavior = new LuaBehavior(define.active, functions);
                    entity.AddBehavior(define.id, behavior);
                }
            }
        }



    }
}

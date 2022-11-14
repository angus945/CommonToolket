using System;
using UnityEngine;
using DataDriven;
using ModdingLaboratory.Definition;
using ModdingLaboratory.Definition.Componentized;
using ModdingLaboratory.Definition.TypeScript;
using ModdingLaboratory.Instance.Behavior;
using ModdingLaboratory.Instance.Visual;
using MoonSharp.Interpreter;

namespace ModdingLaboratory.Instance
{
    public class EntityInstantiator
    {

        public static GameEntity CreateEntity(string entityID)
        {
            Debugger.Tick();

            GameEntity entity = null;

            if (EntityDatabase.TryGetEntity(entityID, out EntityDefine define, out EntityModule[] modules))
            {
                entity = CreateEntity(define, modules);
            }

            Debugger.Tick("Entity Instance");

            return entity;
        }

        static GameEntity CreateEntity(EntityDefine define, EntityModule[] modules)
        {
            GameObject entityObject = new GameObject(define.name);

            GameEntity entity = entityObject.AddComponent<GameEntity>();
            SetIdentifier(entity, define);

            AddModule(entity, define.visuals); //GC 5.4 KB
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
                VisualModule.SpriteSheet sheet = visual[i];

                if(VisualDatabase.TryGetSpriteSheet(sheet.sheetName, out SpriteSheet spriteSheet))
                {
                    entity.AddSpriteSheet(sheet.id, spriteSheet);
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

                if(!entity.ContainsComponent(define.localID))
                {
                    Component component = entity.gameObject.AddComponent(define.RequireComponentType);

                    define.InitialComponent(component);
                    entity.AddComponent(define.localID, component);
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

using UnityEngine;
using ModdingLab.Definition;
using ModdingLab.Definition.Componentized;
using ModdingLab.Instance.Visual;

namespace ModdingLab.Instance
{
    public class EntityInstantiator
    {
        public static GameEntity CreateEntity(string entityID)
        {
            EntityDefine define = DefinitionTables.GetEntityDefine(entityID);

            return CreateEntity(define);
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
            for (int i = 0; i < define.components.Length; i++)
            {
                ComponentDefine componentDefine = define.components[i];

                Component component = entityObject.AddComponent(componentDefine.RequireComponentType);

                componentDefine.InitialComponent(entity, component);
                entity.AddComponent(componentDefine.id, component);
            }

            return entity;
        }
    }
}
